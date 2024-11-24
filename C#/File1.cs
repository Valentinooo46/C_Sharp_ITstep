using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Project2
{
    internal class File1
    {
        static int CountWord(string text,string word)
        {
            Int32 Count = 0;
            Int32 Index = 0;
            Index = text.IndexOf(word, Index);
            while((Index) != -1)
            {
                Count++;
                Index += word.Length;
                Index = text.IndexOf(word, Index);
            }
            return Count;
        }
        static void Main(string[] args)
        {
            //task1 
            Random random = new Random();
            {
                Double max, min;
                Int32[] A = new Int32[5];
                Double[,] B = new Double[3, 4];
                for (int i = 0; i < A.Length; i++)
                {
                    A[i] = Convert.ToInt32(Console.ReadLine());

                }
                max = A[0];
                min = A[0];
                for (int i = 0; i < A.Length; i++)
                {
                    Console.Write($"{A[i]} ");
                    if (A[i] > max)
                    {
                        max = A[i];
                    }
                    else if (A[i] < min)
                    {
                        min = A[i];
                    }
                }
                Console.WriteLine("\n---------------");
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    for (int j = 0; j < B.GetLength(1); j++)
                    {
                        B[i, j] = random.NextDouble() * 100;
                        if (B[i, j] > max)
                        {
                            max = B[i, j];
                        }
                        else if (B[i, j] < min)
                        {
                            min = B[i, j];
                        }

                        Console.Write($"({B[i, j]}) ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Max - {max}\nMin - {min}");


            }
            //task2
            {
                Int32[,] matrix = new Int32[5, 5];
                Int32[] max = new int[2], min = new int[2];
                max[0] = 0; min[0] = 0;
                max[1] = 0; min[1] = 0;
                Int32 sum = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = random.Next(-100, 100);
                        if (matrix[i, j] > matrix[max[0], max[1]])
                        {
                            max[0] = i;
                            max[1] = j;
                        }
                        else if (matrix[i, j] < matrix[min[0], min[1]])
                        {
                            min[0] = i;
                            min[1] = j;
                        }
                        Console.Write($"{matrix[i, j]} ");
                    }
                    Console.WriteLine();

                }
                if (min[0] > max[0])
                {
                    for (int i = max[0]; i <= min[0]; i++)
                    {
                        if (i == max[0])
                        {
                            for (int j = max[1]; j < matrix.GetLength(1); j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                        else if (i == min[0])
                        {
                            for (int j = 0; j <= min[1]; j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                        else
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                    }
                    Console.WriteLine($"Sum - {sum}");
                }
                else
                {
                    for (int i = min[0]; i <= max[0]; i++)
                    {
                        if (i == max[0])
                        {
                            for (int j = 0; j <= max[1]; j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                        else if (i == min[0])
                        {
                            for (int j = min[1]; j < matrix.GetLength(1); j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                        else
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                sum += matrix[i, j];
                            }
                        }
                    }
                    Console.WriteLine($"Sum - {sum}");
                }




            }
            //task 4
            {
                Int32 column, row;
                column = Convert.ToInt32(Console.ReadLine());
                row = Convert.ToInt32(Console.ReadLine());
                Int32[,] matrix = new Int32[column, row];
                char select;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = random.Next(i, j + i);
                        Console.Write($"{matrix[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("M = matrix * matrix;\nN = matrix * value;\nT = matrix + matrix");
                select = Convert.ToChar(Console.ReadLine());

                if (select == 'M')
                {
                    Console.WriteLine("\n----------------------");
                    Int32[,] matrix2 = new Int32[matrix.GetLength(0), matrix.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix2[i, j] = random.Next(i, j + i);
                            matrix[i, j] *= matrix2[i, j];
                            Console.Write($"{matrix2[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n----------------------\nResult:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {


                            Console.Write($"{matrix[i, j]} ");
                        }
                        Console.WriteLine();
                    }

                }
                else if (select == 'T')
                {
                    Console.WriteLine("\n----------------------");
                    Int32[,] matrix2 = new Int32[matrix.GetLength(0), matrix.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix2[i, j] = random.Next(i, j);
                            matrix[i, j] += matrix2[i, j];
                            Console.Write($"{matrix2[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n----------------------\nResult:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {


                            Console.Write($"{matrix[i, j]} ");
                        }
                        Console.WriteLine();
                    }

                }
                else if (select == 'N')
                {

                    Int32 value = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\n----------------------\nResult:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {

                            matrix[i, j] *= value;
                            Console.Write($"{matrix[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }




            }
            //task 5
            {
                string str = Console.ReadLine();
                str = str.Replace(" ", "");
                Boolean IsCorrect = true;

                foreach (char c in str)
                {
                    if (!char.IsDigit(c) && c != '+' && c != '-' && c != '=')
                    {
                        IsCorrect = false;
                        Console.WriteLine("the expression is incorrect");
                        break;
                    }


                }
                if (IsCorrect)
                {

                    double result = 0;
                    Int32 start = 0;
                    Char operation = '+';
                    for (int i = 0; i <= str.Length; i++)
                    {
                        if (i == str.Length || str[i] == '+' || str[i] == '-')
                        {
                            string number = str.Substring(start, i - start);
                            if (double.TryParse(number, out double value))
                            {
                                if (operation == '+')
                                {
                                    result += value;
                                }
                                else if (operation == '-')
                                {
                                    result -= value;
                                }
                            }
                            if (i < str.Length)
                            {
                                operation = str[i];
                                start = i + 1;
                            }
                        }
                    }
                    Console.WriteLine(result);






                }
            }
            //task 3
            {
                string str = Console.ReadLine();
                string new_str = "";
                string new_str2 = "";
                for (int i = 0; i < str.Length; i++)
                {
                    switch (str[i])
                    {
                        //lower
                        case 'a': new_str += 'c'; break;
                        case 'b': new_str += 'd'; break;
                        case 'c': new_str += 'e'; break;
                        case 'd': new_str += 'f'; break;
                        case 'e': new_str += 'g'; break;
                        case 'f': new_str += 'h'; break;
                        case 'g': new_str += 'i'; break;
                        case 'h': new_str += 'j'; break;
                        case 'i': new_str += 'k'; break;
                        case 'j': new_str += 'l'; break;
                        case 'k': new_str += 'm'; break;
                        case 'l': new_str += 'n'; break;
                        case 'm': new_str += 'o'; break;
                        case 'n': new_str += 'p'; break;
                        case 'o': new_str += 'q'; break;
                        case 'p': new_str += 'r'; break;
                        case 'q': new_str += 's'; break;
                        case 'r': new_str += 't'; break;
                        case 's': new_str += 'u'; break;
                        case 't': new_str += 'v'; break;
                        case 'u': new_str += 'w'; break;
                        case 'v': new_str += 'x'; break;
                        case 'w': new_str += 'y'; break;
                        case 'x': new_str += 'z'; break;
                        case 'y': new_str += 'a'; break;
                        case 'z': new_str += 'b'; break;

                        //upper
                        case 'A': new_str += 'C'; break;
                        case 'B': new_str += 'D'; break;
                        case 'C': new_str += 'E'; break;
                        case 'D': new_str += 'F'; break;
                        case 'E': new_str += 'G'; break;
                        case 'F': new_str += 'H'; break;
                        case 'G': new_str += 'I'; break;
                        case 'H': new_str += 'J'; break;
                        case 'I': new_str += 'K'; break;
                        case 'J': new_str += 'L'; break;
                        case 'K': new_str += 'M'; break;
                        case 'L': new_str += 'N'; break;
                        case 'M': new_str += 'O'; break;
                        case 'N': new_str += 'P'; break;
                        case 'O': new_str += 'Q'; break;
                        case 'P': new_str += 'R'; break;
                        case 'Q': new_str += 'S'; break;
                        case 'R': new_str += 'T'; break;
                        case 'S': new_str += 'U'; break;
                        case 'T': new_str += 'V'; break;
                        case 'U': new_str += 'W'; break;
                        case 'V': new_str += 'X'; break;
                        case 'W': new_str += 'Y'; break;
                        case 'X': new_str += 'Z'; break;
                        case 'Y': new_str += 'A'; break;
                        case 'Z': new_str += 'B'; break;

                        default:
                            new_str += str[i];
                            break;

                    }
                }
                Console.WriteLine(new_str);
                for (int i = 0; i < new_str.Length; i++)
                {
                    switch (new_str[i])
                    {
                        //lower
                        case 'c': new_str2 += 'a'; break;
                        case 'd': new_str2 += 'b'; break;
                        case 'e': new_str2 += 'c'; break;
                        case 'f': new_str2 += 'd'; break;
                        case 'g': new_str2 += 'e'; break;
                        case 'h': new_str2 += 'f'; break;
                        case 'i': new_str2 += 'g'; break;
                        case 'j': new_str2 += 'h'; break;
                        case 'k': new_str2 += 'i'; break;
                        case 'l': new_str2 += 'j'; break;
                        case 'm': new_str2 += 'k'; break;
                        case 'n': new_str2 += 'l'; break;
                        case 'o': new_str2 += 'm'; break;
                        case 'p': new_str2 += 'n'; break;
                        case 'q': new_str2 += 'o'; break;
                        case 'r': new_str2 += 'p'; break;
                        case 's': new_str2 += 'q'; break;
                        case 't': new_str2 += 'r'; break;
                        case 'u': new_str2 += 's'; break;
                        case 'v': new_str2 += 't'; break;
                        case 'w': new_str2 += 'u'; break;
                        case 'x': new_str2 += 'v'; break;
                        case 'y': new_str2 += 'w'; break;
                        case 'z': new_str2 += 'x'; break;
                        case 'a': new_str2 += 'y'; break;
                        case 'b': new_str2 += 'z'; break;

                        //upper
                        case 'C': new_str2 += 'A'; break;
                        case 'D': new_str2 += 'B'; break;
                        case 'E': new_str2 += 'C'; break;
                        case 'F': new_str2 += 'D'; break;
                        case 'G': new_str2 += 'E'; break;
                        case 'H': new_str2 += 'F'; break;
                        case 'I': new_str2 += 'G'; break;
                        case 'J': new_str2 += 'H'; break;
                        case 'K': new_str2 += 'I'; break;
                        case 'L': new_str2 += 'J'; break;
                        case 'M': new_str2 += 'K'; break;
                        case 'N': new_str2 += 'L'; break;
                        case 'O': new_str2 += 'M'; break;
                        case 'P': new_str2 += 'N'; break;
                        case 'Q': new_str2 += 'O'; break;
                        case 'R': new_str2 += 'P'; break;
                        case 'S': new_str2 += 'Q'; break;
                        case 'T': new_str2 += 'R'; break;
                        case 'U': new_str2 += 'S'; break;
                        case 'V': new_str2 += 'T'; break;
                        case 'W': new_str2 += 'U'; break;
                        case 'X': new_str2 += 'V'; break;
                        case 'Y': new_str2 += 'W'; break;
                        case 'Z': new_str2 += 'X'; break;
                        case 'A': new_str2 += 'Y'; break;
                        case 'B': new_str2 += 'Z'; break;

                        default:
                            new_str2 += str[i];
                            break;

                    }
                }
                Console.WriteLine(new_str2);
            }
            //task 6
            {
                string str = Console.ReadLine();
                string new_str = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '.' && str[i + 1] != '\0')
                    {
                        new_str += str[i];
                        new_str += Char.ToUpper(str[i + 1]);
                        i++;
                    }
                    else
                    {
                        new_str += str[i];
                    }
                }
                Console.WriteLine(new_str);
            }
            //task 7
            {
                string str = Console.ReadLine();
                string word = Console.ReadLine();
                string new_word = "";
                for (int i = 0; i < word.Length; i++)
                    new_word += '*';
                string new_str = str.Replace(word, new_word);
               Console.WriteLine(new_str);


                Console.WriteLine(CountWord(str, word));

            }
        }
    }
}


