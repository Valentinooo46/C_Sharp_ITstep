using RecipeApp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class File1
    {
        static void Main(string[] args)
        {
            Company comp = new Company();
            comp.Add();
            comp.Add();
            comp.Add();
            comp.Add();
            var SalariesSortedList = from T in comp
                                     orderby T.total_salaries descending
                                     select T;
            foreach(var salaries in SalariesSortedList)
            {
                Console.WriteLine("Весь заробіток відділу:" + salaries.total_salaries + "\nСередній заробіток відділу:" + salaries.average_salary + "\n---------------------------");
            }
            var SalariesUnitSortedList = from T in comp
                                         from U in T
                                     orderby U.total_salary descending
                                     select U;
            foreach(var units_salaries in SalariesUnitSortedList)
            {
                Console.WriteLine("Весь заробіток юніту:" + units_salaries.total_salary);
            }
            Console.WriteLine("----------------------\n");
            var SalariesAverageSortedList = from T in comp
                                     orderby T.max_salary - T.min_salary descending
                                     select T;
            foreach (var average_salaries in SalariesAverageSortedList)
            {
                Console.WriteLine("Розкид по зарплаті відділу:" + (average_salaries.max_salary - average_salaries.min_salary));
            }
            Console.WriteLine("----------------------\n");
            var ShareMansSortedList = from T in comp
                                            orderby T.share_mans descending
                                            select T;
            foreach (var share_mans in ShareMansSortedList)
            {
                Console.WriteLine("Частка чоловіків у відділі:" + (share_mans.share_mans * 100) + "%");
            }
            Console.WriteLine("----------------------\n");
        }
        
    }
}
