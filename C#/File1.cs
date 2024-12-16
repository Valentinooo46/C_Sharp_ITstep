using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class File1
    {
        static void Main(string[] args)
        {
            List_Pets list1 = new List_Pets(10);
            foreach (Pets item in list1) {
                Console.WriteLine(item.Name.ToString() + " " + item.Spice);        
            }
            list1.See();
        }
    }
}
