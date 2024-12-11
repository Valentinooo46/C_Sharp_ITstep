using MyClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace Project2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Tank> list = new List<Tank>();
            for (int i = 0; i < 4; i++) {
                list.Add(new Tank("T-34", "soviet union"));
            }
            List<Tank> list2 = new List<Tank>();
            for (int i = 0; i < 3; i++)
            {
                list2.Add(new Tank("Panther", "nazi germany"));
            }
            Tank.StartGame(list2,list);

        }
    }
    
}


