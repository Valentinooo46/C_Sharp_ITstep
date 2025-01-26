using System;

using System.IO;
namespace Project2
{
    internal class File1
    {
        static void Main(string[] args)
        {
            Phone phone = new Phone();

            phone.Touch(); 
            phone.PressPowerButton(); 
            phone.Touch(); 
            phone.Touch(); 
            phone.PressPowerButton(); 
            client client = new client(new ConsoleLogger());
            client.Log("Message 1");
        }
    }

    
    interface IPhoneState
    {
        void PressPowerButton();
        void Touch();
    }

    
    class PhoneOffState : IPhoneState
    {
        readonly Phone _phone;

        public PhoneOffState(Phone phone)
        {
            _phone = phone;
        }

        public void PressPowerButton()
        {
            Console.WriteLine("Телефон увімкнено, екран вимкнено.");
            _phone.SetState(new PhoneOnScreenOffState(_phone));
        }

        public void Touch()
        {
            Console.WriteLine("Телефон вимкнений. Екран не реагує на дотик.");
        }
    }

    
    class PhoneOnScreenOffState : IPhoneState
    {
        readonly Phone _phone;

        public PhoneOnScreenOffState(Phone phone)
        {
            _phone = phone;
        }

        public void PressPowerButton()
        {
            Console.WriteLine("Телефон вимкнено.");
            _phone.SetState(new PhoneOffState(_phone));
        }

        public void Touch()
        {
            Console.WriteLine("Екран увімкнено.");
            _phone.SetState(new PhoneOnScreenOnState(_phone));
        }
    }

    
    class PhoneOnScreenOnState : IPhoneState
    {
        readonly Phone _phone;

        public PhoneOnScreenOnState(Phone phone)
        {
            _phone = phone;
        }

        public void PressPowerButton()
        {
            Console.WriteLine("Телефон вимкнено.");
            _phone.SetState(new PhoneOffState(_phone));
        }

        public void Touch()
        {
            Console.WriteLine("Реакція на дотик, екран увімкнено.");
        }
    }

    
    class Phone
    {
        IPhoneState _state;

        public Phone()
        {
            _state = new PhoneOffState(this);
        }

        public void SetState(IPhoneState state)
        {
            _state = state;
        }

        public void PressPowerButton()
        {
            _state.PressPowerButton();
        }

        public void Touch()
        {
            _state.Touch();
        }
    }
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogger : ILogger
    {
        readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            File.AppendAllText(_filePath, message + "\n");
        }
    }

    class FileLoggerWithTime : ILogger
    {
        readonly string _filePath;

        public FileLoggerWithTime(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            string messageWithTime = $"{DateTime.Now}: {message}";
            File.AppendAllText(_filePath, messageWithTime + "\n");
        }
    }

    class client
    {
        ILogger _logger;

        public client(ILogger logger)
        {
            _logger = logger;
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}

