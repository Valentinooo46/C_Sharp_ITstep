//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Policy;
//using System.Security.Principal;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Project2
//{
//    internal class Program
//    {
//        static void Main(string[] args) {

//            Bankomat.Bank bank = new Bankomat.Bank("Raiffassen bank");
//            Bankomat.Client client = new Bankomat.Client();
//            client.CreateAccount(bank);
//            Thread.Sleep(1000);
//            Bankomat.Account account = null;
//            for (int i = 0; i < 3 && account == null; i++) {
//                account = client.Entry_Acc(bank);
                
//            }
//            if (account != null)
//            {
//                Char CaseExit = 'N';
//                Char Select;
//                decimal value;
//                Console.WriteLine("The bank client menu welcomes you!\nHere is the menu of functions:\n1 - View the account.\n2 - Add the amount to the account.\n3 - Withdraw the amount from the account.\n4 - Exit the program");
//                do
//                {
//                    Select = Convert.ToChar(Console.ReadLine());
//                    switch (Select)
//                    {
//                        case '1':
//                            account.PrintMajor();
//                            Console.WriteLine("Do you want to exit from program?(Y - yes,N - no)");
//                            CaseExit = Convert.ToChar(Console.ReadLine());
//                            if (CaseExit == 'Y')
//                            {
//                                account.Exit();
//                            }
//                            break;
//                        case '2':
//                            value = Convert.ToDecimal(Console.ReadLine());
//                            account.AddMajor(value);
//                            Console.WriteLine("Do you want to exit from program?(Y - yes,N - no)");
//                            CaseExit = Convert.ToChar(Console.ReadLine());
//                            if (CaseExit == 'Y')
//                            {
//                                account.Exit();
//                            }
//                            break;
//                        case '3':
//                            value = Convert.ToDecimal(Console.ReadLine());
//                            account.GetMajor(value);
//                            Console.WriteLine("Do you want to exit from program?(Y - yes,N - no)");
//                            CaseExit = Convert.ToChar(Console.ReadLine());
//                            if (CaseExit == 'Y')
//                            {
//                                account.Exit();
//                            }
//                            break;
//                        default:
//                            account.Exit();
//                            break;
//                    }


//                } while (account.IsOnLine);
//                Console.WriteLine("See you soon!");

//            }
//            else {
//                Console.WriteLine("You were writing invalid passwords!");
                
//            }
//        }
//    }
//}
//namespace Bankomat
//{
//    class Bank
//    {
//        readonly public string Name_Bank;
//        public Account temp { get; set; }
//        public Bank(string Name_bank)
//        {
//            temp = null;
//            Name_Bank = Name_bank;
//        }
//    }
//    class Client
//    {
//        public void CreateAccount(Bank example)
//        {
//            Random random = new Random();
//            string PIB = Console.ReadLine();
//            string password = Console.ReadLine();
//            decimal major = Convert.ToDecimal(Console.ReadLine());
//            UInt32 ID = Convert.ToUInt32(random.Next(1000000, 9999999));
//            example.temp = new Account(PIB, ID, password, major, example.Name_Bank);

//        }
//        public Account Entry_Acc(Bank example)
//        {
//            string PIB = Console.ReadLine();
//            string password = Console.ReadLine();
//            if (example.temp.PIB == PIB && example.temp.password == password)
//            {
//                example.temp.IsOnLine = true;
//                return example.temp;
//            }
//            else
//            {
//                Console.WriteLine("Invalid password or PIB");
//                return null;
//            }
//        }

//    }
//    class Account
//    {
//        public readonly string PIB;
//        public readonly string password;
//        public decimal major;
//        public readonly UInt32 ID;
//        public readonly string Name_Bank;
//        public bool IsOnLine;
//        public Account(string PIB, UInt32 ID, string password, decimal major, string Name_Bank)
//        {
//            this.PIB = PIB;
//            this.ID = ID;
//            this.password = password;
//            this.major = major;            
//            this.Name_Bank = Name_Bank;
//            IsOnLine = false;

//        }
//        public void PrintMajor()
//        {
//            Console.WriteLine($"Major:{major}");
//        }
//        public void AddMajor(decimal major)
//        {
//            Console.WriteLine($"the following amount of money was added to the account:{major}");
//            this.major += major;
//        }
//        public void GetMajor(decimal major)
//        {
//            if (major > this.major)
//            {
//                Console.WriteLine("he withdrawal request is more than the amount of money in the account");
//                return;
//            }
//            else
//            {
//                this.major -= major;
//                Console.WriteLine($"the following amount of money was withdrawn from the account:{major}");
//                return;
//            }
           
           


//        }
//        public void Exit()
//        {
//            IsOnLine = false;
//        }

//    }
//}

