using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exam_CS
{
    // Абстрактний клас Solution, який реалізує Component
    abstract class Solution : Component
    {
        protected string Title { get; set; }
        protected string Description { get; set; }

        protected Solution(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string GetTitle()
        {
            return Title;
        }

        public abstract void execute();
        public abstract void Display();
    }

    // Інтерфейс Component
    interface Component
    {
        void execute();
        void Display();
        string GetTitle();
    }

    // Клас Food, який представляє страву
    class Food : Solution
    {
        uint grams;

        public Food(string title, string description, uint grams) : base(title, description)
        {
            this.grams = grams;
        }

        public override void execute()
        {
            Console.WriteLine("Food is being prepared");
        }

        public override void Display()
        {
            Console.WriteLine($"{Title}: {Description} - {grams} grams");
        }
    }
    // Клас Drink, який представляє напій
    class Drink : Solution
    {
        uint milliliters;

        public Drink(string title, string description, uint milliliters) : base(title, description)
        {
            this.milliliters = milliliters;
        }

        public override void execute()
        {
            Console.WriteLine("Drink is being prepared");
        }

        public override void Display()
        {
            Console.WriteLine($"{Title}: {Description} - {milliliters} milliliters");
        }
    }
    // Клас MenuCategory, який представляє категорію страв
    class MenuCategory : Component
    {
        public string name;
        public List<Component> components = new List<Component>();

        public MenuCategory(string name)
        {
            this.name = name;
        }

        public void Add(Component component)
        {
            components.Add(component);
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public void AddSubCategory(MenuCategory subCategory)
        {
            components.Add(subCategory);
        }

        public void RemoveSubCategory(string subCategoryName)
        {
            components.RemoveAll(c => c is MenuCategory category && category.name == subCategoryName);
        }

        public void execute()
        {
            Console.WriteLine($"Executing category: {name}");
            foreach (var component in components)
            {
                component.execute();
            }
        }

        public void Display()
        {
            Console.WriteLine(name);
            foreach (var component in components)
            {
                component.Display();
            }
        }

        public string GetTitle()
        {
            return name;
        }

        public Component GetDish(string name)
        {
            foreach (var component in components)
            {
                if (component.GetTitle() == name)
                {
                    return component;
                }
                else if (component is MenuCategory category)
                {
                    var dish = category.GetDish(name);
                    if (dish != null)
                    {
                        return dish;
                    }
                }
            }
            return null;
        }
    }
    // Клас Menu, який представляє меню ресторану
    class Menu : Component
    {
        static Menu instance;
        List<Component> components;

        Menu()
        {
            components = new List<Component>();
        }

        public static Menu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Menu();
                }
                return instance;
            }
        }

        public void Add(Component component)
        {
            components.Add(component);
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public void AddFood(string name, string description, uint grams, string menuCategory)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category && category.name == menuCategory)
                {
                    category.Add(new Food(name, description, grams));
                    return;
                }
            }
            Console.WriteLine($"Category {menuCategory} not found.");
        }

        public void RemoveFood(string name)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category)
                {
                    foreach (var subcomponent in category.components)
                    {
                        if (subcomponent is Food food && food.GetTitle() == name)
                        {
                            category.Remove(subcomponent);
                            return;
                        }
                    }
                }
            }
        }

        public void AddDrink(string name, string description, uint milliliters, string menuCategory)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category && category.name == menuCategory)
                {
                    category.Add(new Drink(name, description, milliliters));
                    return;
                }
            }
            Console.WriteLine($"Category {menuCategory} not found.");
        }

        public void RemoveDrink(string name)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category)
                {
                    foreach (var subcomponent in category.components)
                    {
                        if (subcomponent is Drink drink && drink.GetTitle() == name)
                        {
                            category.Remove(subcomponent);
                            return;
                        }
                    }
                }
            }
        }

        public void AddCategory(string name)
        {
            components.Add(new MenuCategory(name));
        }

        public void RemoveCategory(string name)
        {
            components.RemoveAll(c => c is MenuCategory category && category.name == name);
        }

        public void AddSubCategory(string parentCategoryName, string subCategoryName)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category && category.name == parentCategoryName)
                {
                    category.AddSubCategory(new MenuCategory(subCategoryName));
                    return;
                }
            }
            Console.WriteLine($"Parent category {parentCategoryName} not found.");
        }

        public void RemoveSubCategory(string parentCategoryName, string subCategoryName)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category && category.name == parentCategoryName)
                {
                    category.RemoveSubCategory(subCategoryName);
                    return;
                }
            }
            Console.WriteLine($"Parent category {parentCategoryName} not found.");
        }

        public void execute()
        {
            Console.WriteLine("Executing menu");
            foreach (var component in components)
            {
                component.execute();
            }
        }

        public void Display()
        {
            Console.WriteLine("Restaurant Menu:");
            foreach (var component in components)
            {
                component.Display();
            }
        }

        public Component GetDish(string name)
        {
            foreach (var component in components)
            {
                if (component is MenuCategory category)
                {
                    var dish = category.GetDish(name);
                    if (dish != null)
                    {
                        return dish;
                    }
                }
            }
            return null;
        }

        public string GetTitle()
        {
            return "Menu";
        }
    }
}