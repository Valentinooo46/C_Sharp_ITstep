using Bogus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    internal class Pets
    {
        static Random random = new Random();
        
        public string Name { get; set; }
        public string Spice { get; set; }

        public UInt32 Age { get; set; }
        public UInt32 Weight { get; set; }
        public Pets( string name, string spice, uint age, uint weight)
        {
            
            Name = name;
            Spice = spice;
            Age = age;
            Weight = weight;
        }
        internal void See()
        {
            Thread.Sleep(random.Next(1000, 10000));
            Console.WriteLine($" Тварину з iменем:{Name}, виду:{Spice} - оглянуто!");
            return;
        }
    }

    class List_Pets : IEnumerable
    {
        static Random rand = new Random();
        static string[] Animals = new string[] { "Dog", "Cat", "Snake", "Crocodile", "Bird", "Hedgehog", "Kangoroo" };
        static Faker Faker = new Faker();
        private delegate void SeeDel();
        private event SeeDel SeeEvent;


        public List<Pets> Pets { get; set; }
        public List_Pets(int count)
        {
            Pets = new List<Pets>(count);
            for (int i = 0; i < count; i++)
            {
                Pets.Add(new Pets(Faker.Name.FirstName(), List_Pets.Animals[rand.Next(0, Animals.Length)], (uint)rand.Next(1, 10), (uint)rand.Next(1, 50)));
                SeeEvent += Pets.Last().See;
            }
        }
         IEnumerator IEnumerable.GetEnumerator()
        {
            return Pets.GetEnumerator();
        } 
        public void See()
        {
            SeeEvent?.Invoke();
            Console.WriteLine("Усiх тварин оглянуто!");
        }
    }
}
