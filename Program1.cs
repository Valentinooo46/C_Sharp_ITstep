using System.Net;
using System.Net.Sockets;

namespace обмін_Валют_клієнт
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Встановлюємо кодування консолі на UTF-8 для коректного відображення українських символів
            Console.InputEncoding = System.Text.Encoding.UTF8; // Встановлюємо кодування вводу на UTF-8
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1945);
            TcpClient клієнт = new TcpClient();
            клієнт.Connect(endPoint);
            NetworkStream мережеви_потік = клієнт.GetStream();
            Boolean exit = false;
            byte[] буффер = new byte[1024];
            Int32 кількість_байт;
            string запит;
            Console.WriteLine("Введіть логін:");
            string логін = Console.ReadLine();
            Console.WriteLine("Введіть пароль:");
            string пароль = Console.ReadLine();
            запит = логін + " " + пароль;
            мережеви_потік.Write(System.Text.Encoding.UTF8.GetBytes(запит));
            if (мережеви_потік.CanRead)
            {
                кількість_байт = мережеви_потік.Read(буффер, 0, буффер.Length);
                if (кількість_байт > 0)
                {
                    string відповідь = System.Text.Encoding.UTF8.GetString(буффер, 0, кількість_байт);
                    if (відповідь == "BAD")
                    {
                        Console.WriteLine("Невірний логін або пароль. Завершення роботи клієнта.");
                        мережеви_потік.Close();
                        клієнт.Close();
                        return;
                    }
                    else if (відповідь == "OK")
                    {
                        Console.WriteLine("Успішний вхід до системи.");
                    }
                    else
                    {
                        Console.WriteLine("Невідома відповідь від сервера: " + відповідь);
                        мережеви_потік.Close();
                        клієнт.Close();
                        return;
                    }

                }
                while (!exit)
                {
                    try
                    {


                        Console.WriteLine("Введіть назви валют для отримання курсу або 'exit' для виходу:");
                        запит = Console.ReadLine();
                        if (запит.ToLower() == "exit")
                        {
                            exit = true;
                            мережеви_потік.Close();
                            клієнт.Close();
                            Console.WriteLine("З'єднання закрито. Завершення роботи клієнта.");
                            continue;
                        }

                        мережеви_потік.Write(System.Text.Encoding.UTF8.GetBytes(запит));
                        кількість_байт = мережеви_потік.Read(буффер, 0, буффер.Length);
                        if (кількість_байт > 0)
                        {
                            string відповідь = System.Text.Encoding.UTF8.GetString(буффер, 0, кількість_байт);
                            Console.WriteLine($"Відповідь від сервера: {відповідь}");
                        }
                        else
                        {
                            Console.WriteLine("Сервер не відповідає.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        exit = true;
                    }
                }
            }
        }
    }
}
