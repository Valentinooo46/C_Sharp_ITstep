using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;



namespace Project2
{
    namespace crzr
    {
        class crosses_zeros
        {
            static char[,] field;
            static Random random;
            static crosses_zeros()
            {
                field = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
                random = new Random();
            }
            static bool IsEnding()
            {
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[i, j] == ' ')
                            return false;
                    }
                }
                return true;

            }
            static Int32 Winner()
            {
                //horizontal audit
                if (field[0, 0] == field[0, 1] && field[0, 0] != ' ')
                {
                    if ((field[0, 0] == field[0, 2]))
                    {
                        return (field[0, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }

                }
                if (field[1, 0] == field[1, 1] && field[1, 0] != ' ')
                {
                    if ((field[1, 0] == field[1, 2]))
                    {
                        return (field[1, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }

                }
                if (field[2, 0] == field[2, 1] && field[2, 0] != ' ')
                {
                    if ((field[2, 0] == field[2, 2]))
                    {
                        return (field[2, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }
                }

                //vertical audit
                if (field[0, 0] == field[1, 0] && field[0, 0] != ' ')
                {
                    if ((field[0, 0] == field[2, 0]))
                    {
                        return (field[0, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }

                }
                if (field[0, 1] == field[1, 1] && field[0, 1] != ' ')
                {
                    if ((field[0, 1] == field[2, 1]))
                    {
                        return (field[0, 1] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }

                }
                if (field[0, 2] == field[1, 2] && field[0, 2] != ' ')
                {
                    if ((field[0, 2] == field[2, 2]))
                    {
                        return (field[0, 2] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                    }

                }

                //diagonal audit

                if ((field[0, 0] == field[1, 1]) && (field[0, 0] == field[2, 2]) && field[0, 0] != ' ')
                {
                    return (field[0, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                }
                if ((field[0, 2] == field[1, 1]) && (field[0, 2] == field[2, 0]) && field[0, 2] != ' ')
                {
                    return (field[0, 0] == '+') ? 1 : -1; //1 - crosses win, -1 - zeros win 
                }
                return 0;  // draw
            }
            public static void StartGame(bool WithPC = true)
            {
                Int16 count_lead = 0;
                UInt32 x = 0, y = 0;
                if (WithPC)  // matchmaking
                {
                    if (random.Next() % 2 == 1)
                    {
                        PCLead(ref x, ref y, ref count_lead);
                        Print();
                        Console.WriteLine();
                        while (!IsEnding())
                        {
                            PlayerLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                            PCLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                        }



                        Int32 win = Winner();
                        if (win == 0)
                        {
                            Console.WriteLine("draw");
                        }
                        else if (win == 1)
                        {
                            Console.WriteLine("Player win");
                        }
                        else
                        {
                            Console.WriteLine("PC win");
                        }
                    }
                    else
                    {
                        PlayerLead(ref x, ref y, ref count_lead);
                        Print();
                        Console.WriteLine();
                        while (!IsEnding())
                        {
                            PCLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                            PlayerLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                        }



                        Int32 win = Winner();
                        if (win == 0)
                        {
                            Console.WriteLine("draw");
                        }
                        else if (win == 1)
                        {
                            Console.WriteLine("PC win");
                        }
                        else
                        {
                            Console.WriteLine("Player win");
                        }
                    }
                }
                else
                {
                    if (random.Next() % 2 == 1)
                    {
                        PlayerLead(ref x, ref y, ref count_lead, 2);
                        Print();
                        Console.WriteLine();
                        while (!IsEnding())
                        {
                            PlayerLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                            PlayerLead(ref x, ref y, ref count_lead, 2);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                        }



                        Int32 win = Winner();
                        if (win == 0)
                        {
                            Console.WriteLine("draw");
                        }
                        else if (win == 1)
                        {
                            Console.WriteLine("Player 1 win");
                        }
                        else
                        {
                            Console.WriteLine("Player 2 win");
                        }
                    }
                    else
                    {
                        PlayerLead(ref x, ref y, ref count_lead);
                        Print();
                        Console.WriteLine();
                        while (!IsEnding())
                        {
                            PlayerLead(ref x, ref y, ref count_lead, 2);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                            PlayerLead(ref x, ref y, ref count_lead);
                            Print();
                            Console.WriteLine();
                            if (Winner() != 0)
                                break;
                        }



                        Int32 win = Winner();
                        if (win == 0)
                        {
                            Console.WriteLine("draw");
                        }
                        else if (win == 1)
                        {
                            Console.WriteLine("Player 2 win");
                        }
                        else
                        {
                            Console.WriteLine("Player 1 win");
                        }
                    }
                }
            }
            static void Print()
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j != 1)
                            Console.Write(" " + field[i, j].ToString() + " ");
                        else
                            Console.Write("| " + field[i, j].ToString() + " |");

                    }
                    Console.WriteLine("\n-----------");

                }
                Console.WriteLine($" {field[2, 0]} | {field[2, 1]} | {field[2, 2]} ");

            }
            static void PCLead(ref UInt32 x, ref UInt32 y, ref Int16 count_lead)
            {
                do
                {
                    x = (UInt32)(random.Next(10) % 3);
                    y = (UInt32)(random.Next(10) % 3);
                } while (field[x, y] != ' ');
                if (count_lead % 2 == 0)
                {
                    field[x, y] = '0';
                    count_lead++;

                }
                else
                {
                    field[x, y] = '+';
                    count_lead++;
                }

            }
            static void PlayerLead(ref UInt32 x, ref UInt32 y, ref Int16 count_lead, Int16 Team = 1)
            {
                if (Team == 1)
                {
                    do
                    {
                        Console.WriteLine("Player 1 lead:");
                        x = Convert.ToUInt32(Console.ReadLine());
                        y = Convert.ToUInt32(Console.ReadLine());
                    } while (x - 1 >= field.GetLength(0) || x - 1 < 0 || y - 1 >= field.GetLength(1) || y - 1 < 0 || field[x - 1, y - 1] != ' ');
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Player 2 lead:");
                        x = Convert.ToUInt32(Console.ReadLine());
                        y = Convert.ToUInt32(Console.ReadLine());
                    } while (x - 1 >= field.GetLength(0) || x - 1 < 0 || y - 1 >= field.GetLength(1) || y - 1 < 0 || field[x - 1, y - 1] != ' ');
                }
                if (count_lead % 2 == 0)
                {
                    field[x - 1, y - 1] = '0';
                    count_lead++;

                }
                else
                {
                    field[x - 1, y - 1] = '+';
                    count_lead++;
                }

            }
        }
    }
    namespace morse
    {
        class Morse
        {
            static string str_human;
            static string[] str_morse;
            static Morse()
            {
                str_human = null;
                str_morse = null;
            }
            public enum MorseTable
            {
                a = 97,
                b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
                A = 65, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z

            }
            public static string GetStr_Human
            {
                get
                {
                    return str_human;
                }
            }
            static string Human_To_Morse(char s)
            {
                switch (s)
                {
                    case (char)MorseTable.A: return "._ ";
                    case (char)MorseTable.B: return "_... ";
                    case (char)MorseTable.C: return "_._. ";
                    case (char)MorseTable.D: return "_.. ";
                    case (char)MorseTable.E: return ". ";
                    case (char)MorseTable.F: return ".._. ";
                    case (char)MorseTable.G: return "__. ";
                    case (char)MorseTable.H: return ".... ";
                    case (char)MorseTable.I: return ".. ";
                    case (char)MorseTable.J: return ".___ ";
                    case (char)MorseTable.K: return "_._ ";
                    case (char)MorseTable.L: return "._.. ";
                    case (char)MorseTable.M: return "__ ";
                    case (char)MorseTable.N: return "_. ";
                    case (char)MorseTable.O: return "___ ";
                    case (char)MorseTable.P: return ".__. ";
                    case (char)MorseTable.Q: return "__._ ";
                    case (char)MorseTable.R: return "._. ";
                    case (char)MorseTable.S: return "... ";
                    case (char)MorseTable.T: return "_ ";
                    case (char)MorseTable.U: return ".._ ";
                    case (char)MorseTable.V: return "..._ ";
                    case (char)MorseTable.W: return ".__ ";
                    case (char)MorseTable.X: return "_.._ ";
                    case (char)MorseTable.Y: return "_.__ ";
                    case (char)MorseTable.Z: return "__.. ";
                    default:
                        return s.ToString();
                }

            }
            public static void Init(string str)
            {
                str_human = str;
                str_morse = new string[1];
                for (int i = 0; i < str.Length; i++)
                {
                    Array.Resize(ref str_morse, i + 1);
                    str_morse[i] = Human_To_Morse(str[i]);

                }
            }
            public static void Morse_Init(string str)
            {
                str_human = "";
                str_morse = str.Split(' ', '!', '?', ',');
                for (int i = 0; i < str_morse.Length; i++)
                {
                    if (str_morse[i] == "._")
                    {
                        str_human += 'A';
                    }
                    else if (str_morse[i] == "_...")
                    {
                        str_human += 'B';
                    }
                    else if (str_morse[i] == "_._.")
                    {
                        str_human += 'C';
                    }
                    else if (str_morse[i] == "_..")
                    {
                        str_human += 'D';
                    }
                    else if (str_morse[i] == ".")
                    {
                        str_human += 'E';
                    }
                    else if (str_morse[i] == ".._.")
                    {
                        str_human += 'F';
                    }
                    else if (str_morse[i] == "__.")
                    {
                        str_human += 'G';
                    }
                    else if (str_morse[i] == "....")
                    {
                        str_human += 'H';
                    }
                    else if (str_morse[i] == "..")
                    {
                        str_human += 'I';
                    }
                    else if (str_morse[i] == ".___")
                    {
                        str_human += 'J';
                    }
                    else if (str_morse[i] == "_._")
                    {
                        str_human += 'K';
                    }
                    else if (str_morse[i] == "._..")
                    {
                        str_human += 'L';
                    }
                    else if (str_morse[i] == "__")
                    {
                        str_human += 'M';
                    }
                    else if (str_morse[i] == "_.")
                    {
                        str_human += 'N';
                    }
                    else if (str_morse[i] == "___")
                    {
                        str_human += 'O';
                    }
                    else if (str_morse[i] == ".__.")
                    {
                        str_human += 'P';
                    }
                    else if (str_morse[i] == "__._")
                    {
                        str_human += 'Q';
                    }
                    else if (str_morse[i] == "._.")
                    {
                        str_human += 'R';
                    }
                    else if (str_morse[i] == "...")
                    {
                        str_human += 'S';
                    }
                    else if (str_morse[i] == "_")
                    {
                        str_human += 'T';
                    }
                    else if (str_morse[i] == ".._")
                    {
                        str_human += 'U';
                    }
                    else if (str_morse[i] == "..._")
                    {
                        str_human += 'V';
                    }
                    else if (str_morse[i] == ".__")
                    {
                        str_human += 'W';
                    }
                    else if (str_morse[i] == "_.._")
                    {
                        str_human += 'X';
                    }
                    else if (str_morse[i] == "_.__")
                    {
                        str_human += 'Y';
                    }
                    else if (str_morse[i] == "__..")
                    {
                        str_human += 'Z';
                    }

                }
            }
            public static void Print_Morse()
            {
                for (int i = 0; i < str_morse.Length; i++)
                {
                    Console.Write(str_morse[i]);
                }
                Console.WriteLine();
            }

        }
    }
    internal class File2
    {
        static void Main(string[] args)
        {

            crzr.crosses_zeros.StartGame(false);
            morse.Morse.Init("HELLO WORLD!");
            morse.Morse.Print_Morse();
                

        }
    }
}
