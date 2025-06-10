using System.Collections.Concurrent;
using System.Net.Sockets;

namespace CA1
{
    internal class Program
    {
        static public ConcurrentBag<HandleClientClass> handleClients = new ConcurrentBag<HandleClientClass>();
        static public ConcurrentBag<HandleClientClass> bannedClients = new ConcurrentBag<HandleClientClass>();
        static void Main(string[] args)
        {
            Task.Run(() => ListenForKeyPresses()); // Start listening for key presses in a separate thread
            string login = Console.ReadLine();
            while(login != "admin")
            {
                Console.WriteLine("You Write incorrect login! Write correct");
                login = Console.ReadLine();
            }
            string password = Console.ReadLine();
            while (password != "qwerty1234") {
                Console.WriteLine("You Write incorrect password! Write correct");
                password = Console.ReadLine();
            }

            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1945);
            listener.Start();
            Console.WriteLine("Server started. Waiting for clients...");
            HandleClientClass handleClientClass = null;
            while (true)
            {
                handleClientClass = new HandleClientClass(listener.AcceptTcpClient());
                handleClients.Add(handleClientClass); // Add the new client to the collection
                handleClientClass.Run().ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine($"Error handling client: {t.Exception?.GetBaseException().Message}");
                    }
                });
            }

        }
        static void ListenForKeyPresses()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                Console.WriteLine($"Натиснуто: {key.Key}");
                if (key.Key == ConsoleKey.B)
                {
                    string NickName = Console.ReadLine();
                    var bannedclient = handleClients.FirstOrDefault(c  => c.NickName == NickName);
                    if (bannedclient != null)
                    {
                        
                        bannedClients.Add(bannedclient);
                        Console.WriteLine($"client {NickName} was banned");
                    }
                    else {
                        Console.WriteLine($"client with name {NickName} is not exist");

                    }
                    
                }
            }
        }
    }
    public class HandleClientClass
    {

        public TcpClient Client { get; set; }
        public string NickName { get; set; }
        public string[] rooms { get; set; } // This property is not used in the current implementation, but can be used for room management in the future.
        public NetworkStream Stream { get; set; }
        public ConcurrentQueue<string> InputMessages { get; set; }
        public HandleClientClass(TcpClient client) //registration of the client in the server//message: "Nickname|rooms" where rooms is a comma-separated list of rooms the client is in
        {
            Client = client;
            Stream = client.GetStream();
            int bytesRead;
            byte[] buffer = new byte[1024];
            bytesRead = Stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) throw new Exception("Client disconnected before sending nickname and rooms.");
            string initialMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var parts = initialMessage.Split('|');
            if (parts.Length < 2)
            {
                throw new Exception("Invalid initial message format. Expected 'Nickname|rooms'.");
            }
            NickName = parts[0];
            rooms = parts[1].Split(','); // This can be used for room management in the future.
            InputMessages = new ConcurrentQueue<string>();
        }
        //listen for messages from the client and add them to the InputMessages collection
        public async Task Run()
        {
            int bytesRead;
            string message;
            string responseMessage = string.Empty; // Initialize response message
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    if (!InputMessages.IsEmpty)
                    {


                        while (InputMessages.TryDequeue(out message))
                        {
                            responseMessage += message + "\n";
                        }
                        await Stream.WriteAsync(System.Text.Encoding.UTF8.GetBytes(responseMessage));
                    }
                    else //response empty message to client if no messages in queue
                    { 
                        await Stream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("NONE")); 
                    }
                    bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);//message must be in this format: "R/G|room1/nickname|text"
                    if (bytesRead == 0) break; // Client disconnected
                    message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (message == "exit")
                    {
                        Console.WriteLine("Client requested to exit.");
                        break; // Exit the loop if the client sends "exit"
                    }
                    string[] parts = message.Split('|');
                    if (parts.Length < 3 || parts.Length > 3)
                    {
                        Console.WriteLine("Invalid message format. Expected 'R/G|room1/nickname|text'.");
                        continue; // Skip processing this message
                    }
                    else if (parts[0] != "R" && parts[0] != "G")
                    {
                        Console.WriteLine("Invalid message type. Expected 'R' for room or 'G' for guest.");
                        continue; // Skip processing this message
                    }
                    else if (parts[0] == "R")
                    {
                        //multicast or broadcast
                        if (parts[1] == "MAIN")
                        {
                            foreach (var client in Program.handleClients)
                            {
                                if (client != this && !Program.bannedClients.Contains(client)) // Check if the client is not the sender and not banned
                                {
                                    client.InputMessages.Enqueue($"{parts[1]}:{parts[2]}");
                                }
                            }
                        }
                        else
                        {



                            foreach (var client in Program.handleClients)
                            {
                                if (client.rooms.Contains(parts[1]) && client != this && !Program.bannedClients.Contains(client)) // Check if the client is in the same room and not the sender
                                {
                                    client.InputMessages.Enqueue($"{this.NickName}:{parts[2]}");
                                }
                            }
                        }
                        Console.WriteLine($"Room message sent: {message} to room {parts[1]}");
                    }
                    else if (parts[0] == "G")
                    {
                        //unicast
                        var client = Program.handleClients.FirstOrDefault(c => c.NickName == parts[1]);
                        if (client != null && client != this && !Program.bannedClients.Contains(client)) // Check if the client exists and is not the sender
                        {
                            client.InputMessages.Enqueue($"from {this.NickName}:{parts[2]}");
                            Console.WriteLine($" message sent: {message} to {parts[1]}");
                        }
                        else
                        {
                            Console.WriteLine($"Client {parts[1]} not found or is the sender.");
                        }
                    }




                    Console.WriteLine($"Received: {message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
            }
            Dispose(); // Clean up resources when done

        }
        public void Dispose()
        {
            foreach (var client in Program.handleClients)
            {
                if (client != this) // Don't send the message back to the sender
                {
                    client.InputMessages.Enqueue("Client has left.");
                }
            }
            Stream?.Close();
            Client?.Close();
            InputMessages?.Clear();
            Console.WriteLine("Client disconnected and resources cleaned up.");
        }
    }
}
