using Exam_CS;

class Program
{
    static void Main(string[] args)
    {
        Menu.Instance.Add(new MenuCategory("First dishes"));
        Menu.Instance.AddFood("Borsh", "Ukranian dish", 10, "First dishes");
        Menu.Instance.AddFood("Salad", "Greek salad", 10, "First dishes");
        Menu.Instance.Add(new MenuCategory("Second dishes"));
        Menu.Instance.AddFood("Steak", "Beef steak", 10, "Second dishes");
        Menu.Instance.AddFood("Fish", "Salmon", 10, "Second dishes");
        Menu.Instance.Add(new MenuCategory("Drinks"));
        Menu.Instance.AddDrink("Coca-cola", "Coca-cola", 1000, "Drinks");
        Menu.Instance.AddDrink("Fanta", "Fanta", 1000, "Drinks");
        Officiant officiant = new Officiant("Ivanov Ivan Ivanovich", "+38099999999");
        officiant.SetBuilder(new OrderBuilder(officiant));

        Order order = officiant.BuildOrder();
        Client client = new Client();
        client.ExecuteCommand(new AddDishCommand(order, Menu.Instance.GetDish("Borsh")));


    }
}

