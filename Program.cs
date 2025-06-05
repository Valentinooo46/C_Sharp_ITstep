using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Обмін_Валют_Додаток
{
    internal class Program
    {
        static ConcurrentDictionary<string, decimal> курсиВалют = new ConcurrentDictionary<string, decimal>();
        static ConcurrentDictionary<TcpClient, byte> кількість_звернень = new ConcurrentDictionary<TcpClient, byte>();
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        static readonly ConcurrentQueue<string> черга_логів = new(); //потокобезпечна черга для логів
        static readonly AutoResetEvent подія_логування = new(false); //

        static void Main(string[] args)
        {
            string Login = "admin";
            string Password = "qwery123";
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Встановлюємо кодування консолі на UTF-8 для коректного відображення українських символів
            Console.InputEncoding = System.Text.Encoding.UTF8; // Встановлюємо кодування вводу на UTF-8
            StartLogWriter(); // Запускаємо фоновий запис логів
            TcpListener прослуховувач = new TcpListener(System.Net.IPAddress.Any, 1945);
            прослуховувач.Start();
            Task.Run(() => ПеревіркаКлавіш(cancellationTokenSource.Token, прослуховувач));
            Console.WriteLine("Сервер запущено. Очікування підключень...");
            /// Цикл для прийому підключень від клієнтів
            while (true)
            {
                try
                {
                    _ = ОбробкаЗапиту(прослуховувач.AcceptTcpClient());
                }
                catch (SocketException)
                {
                    // Прослуховувач зупинено, вихід із циклу
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        static void додатиВалюту()
        {
            string валюта; decimal курс;
            Console.Write("Введіть назву валюти: ");
            валюта = Console.ReadLine();
            Console.Write("Введіть курс валюти: ");
            while (!decimal.TryParse(Console.ReadLine(), out курс) || курс <= 0)
            {
                Console.Write("Невірний курс. Введіть курс валюти знову: ");
            }
            if (курсиВалют.TryAdd(валюта, курс))
            {
                Console.WriteLine($"Валюта {валюта} з курсом {курс} додана.");
            }
            else
            {
                Console.WriteLine($"Валюта {валюта} вже існує.");
            }
        }
        /// <summary>
        /// Перевірка клавіш для додавання валюти або завершення роботи сервера.
        /// </summary>
        /// <param name="token">токен для завершння задачі</param>
        /// <param name="прослуховувач">TcpListener для завершення прослуховування порту 1945</param>
        /// <returns></returns>
        static async Task ПеревіркаКлавіш(CancellationToken token, TcpListener прослуховувач)
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.A)
                    {
                        додатиВалюту();
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        cancellationTokenSource.Cancel();
                        прослуховувач.Stop(); // Зупиняємо прослуховувач
                        string message = "Сервер завершив роботу";
                        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                        foreach (var client in кількість_звернень.Keys)
                        {
                            try
                            {
                                if (client.Connected)
                                {
                                    using (NetworkStream stream = client.GetStream())
                                    {
                                        await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                                    }
                                }
                                //видалення списку клієнтів
                                кількість_звернень.TryRemove(client, out _);
                                Console.WriteLine($"Клієнт {client.Client.RemoteEndPoint} відключено та видалено зі списку.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка при надсиланні повідомлення клієнту: {ex.Message}");
                            }
                        }

                        break; // Вихід з циклу перевірки клавіш
                    }
                    // Додайте інші перевірки клавіш за потреби
                }
                await Task.Delay(1000); // Затримка для зниження навантаження на CPU
            }
        }
        static void StartLogWriter()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    подія_логування.WaitOne();
                    while (черга_логів.TryDequeue(out var log))
                    {
                        File.AppendAllText("...\\Обмін_Валют_Додаток\\source\\logs.txt", log + Environment.NewLine); //Environment.NewLine - перенос рядка(для різних ОС повертає різне значення)
                    }
                }
            });
        }

        static void LogToFile(string message)
        {
            черга_логів.Enqueue($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}"); //додаємо лог до черги(не блокує потік)
            подія_логування.Set(); //піднімаємо прапорець
        }
        static async Task ОбробкаЗапиту(TcpClient клієнт)
        {
            Console.WriteLine("Клієнт підключено.");
            кількість_звернень.TryAdd(клієнт, 0); //додаємо клієнта до словника
            NetworkStream мережевий_потік = клієнт.GetStream();
            byte[] buffer = new byte[1024];
            Int32 bytesRead;
            string[] валюти;
            string відповідь = string.Empty;
            Boolean exit = false;
            //перевірка логіну та паролю(використаємо наявний массив для валют//валюти[0] - логін|валюти[1] - пароль)
            bytesRead = await мережевий_потік.ReadAsync(buffer, 0, buffer.Length);
            валюти = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead).Split(' ');
            if(валюти.Length != 2 || валюти[0] != "admin" || валюти[1] != "qwery123")
            {
                await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes("BAD"));
                
                LogToFile($"Клієнт {клієнт.Client.RemoteEndPoint} намагався підключитися з невірними обліковими даними.");
                мережевий_потік.Close(); // Закриваємо мережевий потік
                клієнт.Close(); // Закриваємо з'єднання з клієнтом
                return; // Завершуємо обробку запиту
            }
            else
            {
                await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes("OK"));
                // Логування підключення клієнта
                LogToFile($"Клієнт {клієнт.Client.RemoteEndPoint} підключився.");
                
            }
            // Цикл для обробки запитів від клієнта
            try
            {
                while (!exit)
                {



                    bytesRead = await мережевий_потік.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Клієнт відключився.");
                        break; // Вихід з циклу, якщо клієнт відключився
                    }
                    валюти = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead).Split(' ');
                    switch (валюти.Length)
                    {
                        case 0:
                            Console.WriteLine("Отримано порожній запит.");
                            continue; // Пропускаємо порожні запити

                        case 1 when валюти[0].ToLower() == "вихід" || валюти[0].ToLower() == "exit":
                            Console.WriteLine("Клієнт надіслав команду виходу.");
                            await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Ви вийшли з програми.\n"));
                            мережевий_потік.Close(); // Закриваємо мережевий потік
                            LogToFile($"Клієнт {клієнт.Client.RemoteEndPoint} надіслав команду виходу.");
                            клієнт.Close(); // Закриваємо з'єднання з клієнтом
                            exit = true;
                            break; // Вихід з циклу, якщо клієнт надіслав команду виходу

                        case 2 when курсиВалют.ContainsKey(валюти[0]) && курсиВалют.ContainsKey(валюти[1]):
                            if (кількість_звернень[клієнт] > 5)
                            {

                                await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Ви перевищили ліміт запитів. З'єднання закрито на 1хв.\n"));

                                LogToFile($"Клієнт {клієнт.Client.RemoteEndPoint} перевищив ліміт запитів і був призупинений на 1хв.");
                                await Task.Delay(TimeSpan.FromMinutes(1)); // Затримка на 1 хвилину
                                //відновлення ліміту
                                кількість_звернень[клієнт] = 0; // Скидаємо ліміт запитів
                                break;
                            }
                            LogToFile($"Клієнт {клієнт.Client.RemoteEndPoint} запитав курс з {валюти[0]} на {валюти[1]}.");
                            // Якщо валюти вказані правильно, обчислюємо курс
                            відповідь = $"{валюти[0]} -> {валюти[1]}: {курсиВалют[валюти[1]] / курсиВалют[валюти[0]]}\n";
                            await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes(відповідь));
                            //додаємо 1 до к-ості звернень клієнта
                            кількість_звернень.AddOrUpdate(клієнт, 1, (key, oldValue) => (byte)(oldValue + 1));
                            break;

                        default:
                            // Якщо запит некоректний, надсилаємо повідомлення про помилку
                            Console.WriteLine("Отримано некоректний запит.");
                            await мережевий_потік.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Некоректний запит. Будь ласка, введіть дійсні дві валюти.\n"));
                            break;
                    }


                }
                кількість_звернень.TryRemove(клієнт, out _); // Видаляємо клієнта зі словника
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка обробки запиту: {e.Message}");
                return;
            }

        }
    }
}
