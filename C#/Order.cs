using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CS
{
    class Order
    {
        public List<Component> dishes;
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string PIB_officiant { get; set; }

        public Order()
        {
            dishes = new List<Component>();
        }

        public Order(List<Component> dishes, string adress, string phone, string pib_officiant)
        {
            this.dishes = dishes;
            this.Adress = adress;
            this.Phone = phone;
            this.PIB_officiant = pib_officiant;
        }
    }
    interface IBuilder
    {
        void AddDish(string Name);
        void SetOfficiant();
        void SetAdress(string adress);

        void SetPhone();

        Order GetResult();

    }

    class OrderBuilder : IBuilder
    {
        Order order;
        Officiant officiant;
        public OrderBuilder(Officiant officiant)
        {
            order = new Order();
            this.officiant = officiant;
        }
        public void AddDish(string Name)
        {
            if (Menu.Instance.GetDish(Name) != null)
            {
                order.dishes.Add(Menu.Instance.GetDish(Name));
            }
            else
            {
                Console.WriteLine("Dish not found");
            }
        }
        public void SetOfficiant()
        {
            order.PIB_officiant = officiant.PIB;
        }
        public void SetAdress(string adress)
        {
            order.Adress = adress;
        }
        public void SetPhone()
        {
            order.Phone = officiant.phone;
        }
        public Order GetResult()
        {
            order.dishes = new List<Component>();
            Boolean exit = false;
            string choice;
            do
            {
                Console.WriteLine("Write the dish name");
                choice = Console.ReadLine();
                AddDish(choice);
                Console.WriteLine("Do you want to add another dish? (yes/no)");
                choice = Console.ReadLine();
                if (choice == "no")
                {
                    exit = true;
                }
            } while (!exit);
            SetOfficiant();
            Console.WriteLine("Write the adress");
            choice = Console.ReadLine();
            SetAdress(choice);
            SetPhone();

            return order;
        }

    }
}
