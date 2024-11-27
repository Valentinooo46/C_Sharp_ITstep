using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Project2
{

    class Employee
    {
        string PIB;
        string Date_Birthday;
        string Fax;
        string email;
        string role;
        string work_duty;
        public Employee(string pIB, string date_Birthday, string fax, string email, string role, string work_duty)
        {
            PIB = pIB;
            Date_Birthday = date_Birthday;
            Fax = fax;
            this.email = email;
            this.role = role;
            this.work_duty = work_duty;
        }
        public ref string GetSetPIB()
        {
            return ref PIB;
        }
        public ref string GetSetDate_Birthday()
        {
            return ref Date_Birthday;
        }
        public ref string GetSetFax()
        {
            return ref Fax;
        }
        public ref string GetSetEmail()
        {
            return ref email;
        }
        public ref string GetSetRole()
        {
            return ref role;
        }
        public ref string GetSetWork_duty()
        {
            return ref work_duty;
        }
        public override string ToString()
        {
            return "PIB:\n" + PIB + "\nDate Birtaday:\n" + Date_Birthday + "\nFax:\n" + Fax + "\nemail:\n" + email + "\nrole:\n" + role + "\nwork_duty:\n" + work_duty;
        }
    }
    class Aircraft
    {
        string Name, Producer_Name;
        UInt32 date_produce;
        string Type;
        public Aircraft(string Name, string Producer_Name, UInt32 date_produce, string Type)
        {
            this.Name = Name;
            this.Producer_Name = Producer_Name;
            this.date_produce = date_produce;
            this.Type = Type;

        }
        public ref string GetSetName()
        {
            return ref Name;
        }
        public ref string GetSetProducerName()
        {
            return ref Producer_Name;
        }
        public ref string GetSetType()
        {
            return ref Type;
        }
        public ref UInt32 GetSetDateProduce()
        {
            return ref date_produce;
        }
        public override String ToString()
        {
            return Name + " " + Producer_Name + " " + date_produce.ToString() + " " + Type;
        }
        public void ToString(out string value)
        {
            value = Name + " " + Producer_Name + " " + date_produce.ToString() + " " + Type;
        }
    }

    class Matrix
    {
        int[,] ints;
        public Matrix(UInt32 size)
        {
            Random random = new Random();
            ints = new int[size, size];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    ints[i, j] = random.Next(1000);
                    Console.Write(ints[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        public UInt32 GetLen
        {
            get
            {
                return (UInt32)ints.GetLength(0);
            }
        }
        
        public Matrix(UInt32 size, UInt32 Max)
        {
            Random random = new Random();
            ints = new int[size, size];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    ints[i, j] = random.Next((int)Max);
                    Console.Write(ints[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        public Matrix(UInt32 size, UInt32 Min, UInt32 Max)
        {
            Random random = new Random();
            if (Max < Min)
            {
                UInt32 temp = Max;
                Max = Min;
                Min = temp;
            }
            ints = new int[size, size];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    ints[i, j] = random.Next((int)Min, (int)Max);
                    Console.Write(ints[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        public Int32 Max()
        {
            Int32 max = ints[0, 0];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    if (ints[i, j] > max)
                        max = ints[i, j];
                }
            }
            return max;
        }
        public void Max(out Int32 max)
        {
            max = ints[0, 0];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    if (ints[i, j] > max)
                        max = ints[i, j];
                }
            }
            
        }
        public Int32 Min()
        {
            Int32 min = ints[0, 0];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    if (ints[i, j] < min)
                        min = ints[i, j];
                }
            }
            return min;
        }
        public void Min(out Int32 min)
        {
            min = ints[0, 0];
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    if (ints[i, j] < min)
                        min = ints[i, j];
                }
            }

        }
        public void InputValue(Int32 value,UInt32 X,UInt32 Y)
        {
            if (X > ints.GetLength(0) || X < 0)
            {
                Console.WriteLine("X out of range!");
                return;
            }
            else if (Y > ints.GetLength(1) || Y < 0)
            {
                Console.WriteLine("Y out of range!");
                return;
            }
            else
            {
                ints[X,Y] = value;
                return;
            }
        }
        public void InputValue(out Int32 value, UInt32 X, UInt32 Y)
        {
            if (X > ints.GetLength(0) || X < 0)
            {
                Console.WriteLine("X out of range!");
                value = -1;
                return;
            }
            else if (Y > ints.GetLength(1) || Y < 0)
            {
                Console.WriteLine("Y out of range!");
                value = -1;
                return;
            }
            else
            {
                value = ints[X,Y];
                return;
            }
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < ints.GetLength(0); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    str += ints[i, j].ToString() + " "; 
                }
                str += "\n---------------------------\n";
            }
            return str;

        }
        public void Print() { 
            for (int i = 0;i < ints.GetLength(0); i++)
            {
                for(int j = 0;j < ints.GetLength(1); j++)
                {
                    Console.Write(ints[i, j].ToString() + " ");
                }
                Console.WriteLine("\n---------------------------");
            }
        }

    }

    internal class File2
    {
        static void Main(string[] args)
        {
            Employee em1 = new Employee("Valentin000", "01.09.20015", "+38099999999", "kachay_virus@gmail.com", "Electrician Engineer", "repair hosting systems of company");
            em1.GetSetWork_duty() = "Eat.Drink.Sleep.Think";
            Console.WriteLine(em1.ToString());
            Aircraft pl1 = new Aircraft("SR - 71", "USA military forces", 1990, "Bombardier");
            Console.WriteLine(pl1.ToString());
            string str;
            pl1.GetSetDateProduce() = 2009;
            pl1.ToString(out str);
            Console.WriteLine(str);
            Matrix mat = new Matrix(10);
            Console.WriteLine("*********************************");
            Int32 val;
            mat.InputValue(out val, 1, 0);
            mat.Print();
            Console.WriteLine("*********************************");
            mat.InputValue(val, 2, 0);
            Console.WriteLine(mat.ToString());
            Console.WriteLine(val);
            
        }
    }
}
