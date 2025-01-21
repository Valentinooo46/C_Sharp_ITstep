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
            ITarget adapter = new Adapter();
            adapter.ClientDouble(1.5); 
            adapter.ClientInt(2);      
            adapter.ClientChar('A');   
        }


    }
    interface ITarget
    {
        void ClientDouble(Double val);
        void ClientInt(Int32 val);
        void ClientChar(Char val);
    }
    class Adapter :Original,ITarget
    {
        
        public void ClientDouble(Double val)
        {
            OriginalDouble(val * 2);
        }
        public void ClientInt(Int32 val)
        {
            OriginalInt(val * 4);
        }
        public void ClientChar(Char val)
        {
            for (int i = 0; i < 5; i++)
            {
                OriginalChar(val);
            }
           
        }
    }
    class Original
    {
        public void OriginalDouble(Double val)
        {
            Console.WriteLine($"{val}");
        }
        public void OriginalInt(Int32 val)
        {
            Console.WriteLine($"{val}");
        }
        public void OriginalChar(Char val)
        {
            Console.WriteLine($"{val}");
        }
    }
    class Client: ITarget
    {
        public void ClientDouble(Double val)
        {
            Console.WriteLine($"{val}");
        }
        public void ClientInt(Int32 val)
        {
            Console.WriteLine($"{val}");
        }
        public void ClientChar(Char val)
        {
            Console.WriteLine($"{val}");
        }
    }

}
