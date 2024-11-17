using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    internal class Program
    {

        static void Main(string[] args)
        {
            //task 1
            int value;
            value = Convert.ToInt32(Console.ReadLine());
            if (value <= 100 && value >= 1)
            {
                if (value % 3 == 0 && value % 5 == 0)
                {
                    Console.WriteLine("Fizz Buzz");

                }
                else if (value % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (value % 5 == 0)
                {
                    Console.WriteLine("Buzz");

                }
                else
                {
                    Console.WriteLine(value);
                }
            }
            else
            {
                Console.WriteLine("exception: value must be in range from 1 to 100");
            }
            ////task 2
            int numerator, denominator;
            numerator = Convert.ToInt32(Console.ReadLine());
            denominator = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("{0}% from {1} = {2}", denominator, numerator, (numerator * denominator) / 100.0);
            //task 3
            char NumberOne, NumberTwo, NumberThree, NumberFour;
            int num;
            NumberOne = Convert.ToChar(Console.ReadLine());
            NumberTwo = Convert.ToChar(Console.ReadLine());
            NumberThree = Convert.ToChar(Console.ReadLine());
            NumberFour = Convert.ToChar(Console.ReadLine());
            num = Convert.ToInt32(NumberOne.ToString() + NumberTwo.ToString() + NumberThree.ToString() + NumberFour.ToString());
            Console.WriteLine(num);
            //task 4
            string GexNum = Console.ReadLine();
            if (GexNum.Length == 6)
            {
                char[] arr = new char[GexNum.Length];
                if (GexNum.Length == 6)
                {
                    NumberOne = Convert.ToChar(Console.ReadLine());
                    NumberTwo = Convert.ToChar(Console.ReadLine());
                    for (int i = 0; i < GexNum.Length; i++)
                    {
                        if (i == Convert.ToInt32(Convert.ToString(NumberOne)) - 1)
                        {
                            arr[i] = GexNum[Convert.ToInt32(Convert.ToString(NumberTwo)) - 1];
                        }
                        else if (i == Convert.ToInt32(Convert.ToString(NumberTwo)) - 1)
                        {
                            arr[i] = GexNum[Convert.ToInt32(Convert.ToString(NumberOne)) - 1];
                        }
                        else
                        {
                            arr[i] = GexNum[i];
                        }
                    }
                }
                string NewNum = new string(arr);
                Console.WriteLine(NewNum);
            }
            else
            {
                Console.WriteLine("Invalid number");
            }
            //task 5
            string date = Console.ReadLine();
            DateTime Time = Convert.ToDateTime(date);
            switch (Time.Month)
            {
                case 12: case 1: case 2: Console.WriteLine("Winter"); break;
                case 3: case 4: case 5: Console.WriteLine("Spring"); break;
                case 6: case 7: case 8: Console.WriteLine("Summer"); break;
                case 9: case 10: case 11: Console.WriteLine("Autumn"); break;
                default:
                    break;
            }
            Console.WriteLine(Time.DayOfWeek);
            //task 6
            Single temperature = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Select the operation: F - From Celsius to Farenheit\nC - From Farenheit to Celsius");

            switch (Console.ReadLine())
            {
                case "C": temperature = (temperature - 32) / 1.8F; Console.WriteLine($"{temperature} degrees in Celsius"); break;
                case "F": temperature = (temperature * 1.8F) + 32; Console.WriteLine($"{temperature} degrees in Farenheit"); break;
                default:
                    Console.WriteLine("Invalid variant");
                    break;
            }
            //task 7
            Int32 a, b;
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (a > b)
            {
                for (int i = b; i <= a; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
            else
            {
                for (int i = a; i <= b; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.Write($"{i} ");
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
