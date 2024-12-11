using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyClassLib
{
    public class Tank
    {
        string Model;
        string Nation;
        Int16 Armor_Level;
        Int16 Penetration_Level;
        Int16 Maneuverability_level;
        UInt16 Ammunition;
        static Random random = new Random();
        static UInt16 Draw_Iterator = 0;
        public Tank(string model, string nation, short armor_Level, short maneuverability_level, ushort ammunition, short penetration_Level)
        {
            Model = model;
            Nation = nation;
            Armor_Level = armor_Level;
            Maneuverability_level = maneuverability_level;
            Ammunition = ammunition;
            Penetration_Level = penetration_Level;
        }


        //options of generate



        public Tank(string model, string nation)
        {
            this.Model = model;
            Nation = nation;

            Armor_Level = (Int16)random.Next(50, 100);
            Maneuverability_level = (Int16)random.Next(15,30);
            Ammunition = (UInt16)random.Next(1, 1);
            Penetration_Level = (Int16)random.Next(10, 55);

        }
        public override string ToString()
        {
            return this.Armor_Level.ToString() + " - Armor " + this.Maneuverability_level.ToString() + " - Maneuverability level " + this.Model + " - Model " + this.Nation + " - Nation";
        }








        public static Boolean operator *(Tank left, Tank right)
        {
            if (right.Ammunition != 0)
                right.Ammunition--;
            else
            {
                Console.WriteLine($"{left.Model} doesn`t have a bullets");
                return false;
            }
            if (!RicochetChecker.Check((double)right.Maneuverability_level / left.Penetration_Level))
            {
                right.Armor_Level -= left.Penetration_Level;
                if (right.Armor_Level < 0)
                {
                    Console.WriteLine($"{right.Model} of Nation {right.Nation}: defeated");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{right.Model} has damaged: {left.Penetration_Level}");
                    Console.WriteLine(right.ToString());
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Armor of {right.Model} has ricocheted by bullet of {left.Model}");
                right.Maneuverability_level -= 5;
                return false;
            }
            
                

        }
        public static void StartGame(List<Tank> Nation1, List<Tank> Nation2)
        {
            bool Islead = false; //Nation1(Initiator) - false,Nation2(defender) - true
            Int32 agressror = 0;
            Int32 defender = 0;
            while (Nation1.Count > 0 && Nation2.Count > 0)
            {
                if (Draw(Nation1, Nation2))
                {
                    Console.WriteLine("draw!");
                    break;
                }
                if (!Islead)
                {
                    agressror = random.Next(0, Nation1.Count);
                    defender = random.Next(0, Nation2.Count);
                    if (Nation1[agressror] * Nation2[defender])
                    {
                        Nation2.RemoveAt(defender);

                    }
                    Islead = true;

                }
                else
                {
                    agressror = random.Next(0, Nation2.Count);
                    defender = random.Next(0, Nation1.Count);
                    if (Nation2[agressror] * Nation1[defender])
                    {
                        Nation1.RemoveAt(defender);

                    }
                    Islead = false;


                }
                Thread.Sleep(1000);

            }
            if (Nation1.Count == 0) {
                Console.WriteLine($"defenders win!");
                return;
            
            }
            else if (Nation2.Count == 0) {
            
                Console.WriteLine($"agressors win!");
                return;
            }

        }
        static bool Draw(List<Tank> list, List<Tank> list2) {
            if (Draw_Iterator == 8)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Ammunition != 0)
                    {
                        Draw_Iterator = 0;
                        return false;
                    }

                }
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].Ammunition != 0)
                    {
                        Draw_Iterator = 0;
                        return false;
                    }
                }
                return true;

            }
            else
            {
                Draw_Iterator++;
                return false;
            }
        
        }
    }


    class RicochetChecker  //механіка рикошету(маневреність / рівень бронепробиття)
    {
        static Random random = new Random();

        public static bool Check(double probability)
        {
            if (probability > 1)
            {
                return true;
            }



            return random.NextDouble() < probability;

        }

    }




}
