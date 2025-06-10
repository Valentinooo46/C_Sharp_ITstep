using System.Net.Sockets;

namespace CA1Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient("localhost", 1945);
            Console.WriteLine("Enter your nickname: ");
            string NickName = Console.ReadLine();
            Console.WriteLine("Enter the rooms you want to join (comma-separated): ");
            string[] rooms = Console.ReadLine().Split(',');
            var networkStream = tcpClient.GetStream();
            networkStream.Write(System.Text.Encoding.UTF8.GetBytes($"{NickName}|{string.Join(",", rooms)}"));
            string message = string.Empty;
            string target = string.Empty; // This can be used for room management in the future.
                                          // Додайте цей код у Main перед основним циклом
            Task.Run(() =>
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        if (response != "NONE")
                        {
                            Console.WriteLine($"[Server]: {response}");
                        }
                    }
                }
            });
           


            while (true) { 
                Console.WriteLine("Enter a message to send (or type 'exit' to quit): ");
                message = Console.ReadLine();
                if (message.ToLower() == "exit")
                {
                    break;
                }
                Console.WriteLine("Enter the message type: R-to room,G- to guest");
                target = Console.ReadLine();
                if (target == "R")
                {
                    Console.WriteLine("Enter the room name(or press Enter for broadcast): ");
                    target = Console.ReadLine();
                    if (string.IsNullOrEmpty(target))
                    {
                        message = $"R|MAIN|{message}";
                    }
                    else
                    {
                        message = $"R|{target}|{message}";
                    }
                }
                else if (target == "G")
                {
                    Console.WriteLine("Enter the guest name: ");
                    target = Console.ReadLine();
                    message = $"G|{target}|{message}";
                }
                else
                {
                    Console.WriteLine("Invalid target type. Please enter 'R' for room or 'G' for guest.");
                    continue;
                }
                
                networkStream.Write(System.Text.Encoding.UTF8.GetBytes(message));
                Console.WriteLine($"Message sent: {message}");
                // Optionally, you can read a response from the server
               
                
            }

        }
    }
}
