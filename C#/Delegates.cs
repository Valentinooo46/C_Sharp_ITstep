using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            task1.StaticDelegate = Console.WriteLine;
            task1.StaticDelegate("Hello World!");
            task2.StaticDelegate = Multiple;
            Console.WriteLine(task2.StaticDelegate(10, -9));
            task2.StaticDelegate = Sum;
            Console.WriteLine(task2.StaticDelegate(10, -9));
            task2.StaticDelegate = Subtract;
            Console.WriteLine(task2.StaticDelegate(10, -9));
            task3.StaticDelegate = IsEven;
            if (task3.StaticDelegate(10))
            {
                Console.WriteLine("Even");
            }
            task3.StaticDelegate = IsOdd;
            if (task3.StaticDelegate(11))
            {
                Console.WriteLine("Odd");
            }
            task3.StaticDelegate = IsPrime;
            if (task3.StaticDelegate(73))
            {
                Console.WriteLine("Prime");
            }
            task3.StaticDelegate = IsFibonacci;
            if (task3.StaticDelegate(89))
            {
                Console.WriteLine("Fibonacci");
            }
            if (!task3.StaticDelegate(69))
            {
                Console.WriteLine("not Fibonacci");
            }


        }
        static Int32 Multiple(Int32 x, Int32 y)
        {
            return x * y;

        }
        static Int32 Sum(Int32 x, Int32 y)
        {
            return x + y;

        }
        static Int32 Subtract(Int32 x, Int32 y)
        {
            return x - y;

        }
        static Boolean IsEven(Int32 x)
        {
            return (x % 2 == 0) ? true : false;
        }
        static Boolean IsOdd(Int32 x)
        {
            return (x % 2 == 0) ? false : true;
        }
        static Boolean IsPrime(Int32 x)
        {
            UInt16 count = 0;
            if (x < 0)
            {
                for (Int32 i = x; i < 0; i++)
                {
                    if (x % i == 0) count++;

                }
                if (count <= 2)

                    return true;

                else
                    return false;
            }
            else if (x > 0)
            {
                for (Int32 i = 1; i < x; i++)
                {
                    if (x % i == 0) count++;

                }
                if (count <= 2)

                    return true;

                else
                    return false;
            }
            else
            {
                return true;
            }
        }
        static Boolean IsFibonacci(Int32 number)
        {
            // Перевіряємо чи є number додатним числом
            if (number < 0) return false; // Перевіряємо чи number є 0 або 1
            if (number == 0 || number == 1) return true;
            // Два початкові числа Фібоначчі
            int a = 0; int b = 1; // Обчислюємо числа Фібоначчі, поки не досягнемо або перевершимо задане число
            while (b < number) { int temp = a + b; a = b; b = temp; }
            // Перевіряємо чи досягнуте число є рівним заданому числу
            return b == number;

        }
        class task1
        {
            public delegate void PrintDelegate(string str);
            static public PrintDelegate StaticDelegate { get; set; }
            static task1()
            {
                StaticDelegate = null;
            }
        }
        class task2
        {
            public delegate Int32 AriphmeticDelegate(Int32 value, Int32 value2);
            static public AriphmeticDelegate StaticDelegate { get; set; }
            static task2()
            {
                StaticDelegate = null;
            }

        }
        class task3
        {
            public delegate Boolean NumericDelegate(int value);
            static public NumericDelegate StaticDelegate { get; set; }
            static task3()
            {
                StaticDelegate = null;
            }

        }
    }
}
