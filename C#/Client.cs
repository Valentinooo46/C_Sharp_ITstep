using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CS
{
    internal class Client
    {
         List<ICommand> commandHistory = new List<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Add(command);
        }

        public void UndoLastCommand()
        {
            if (commandHistory.Count > 0)
            {
                var lastCommand = commandHistory.Last();
                lastCommand.Undo();
                commandHistory.Remove(lastCommand);
            }
        }
    }
    internal interface ICommand
    {
        void Execute();
        void Undo();
    }
    internal class AddDishCommand : ICommand
    {
        Order order;
        Component dish;

        public AddDishCommand(Order order, Component dish)
        {
            this.order = order;
            this.dish = dish;
        }

        public void Execute()
        {
            order.dishes.Add(dish);
            Console.WriteLine($"Added {dish.GetTitle()} to the order.");
        }

        public void Undo()
        {
            order.dishes.Remove(dish);
            Console.WriteLine($"Removed {dish.GetTitle()} from the order.");
        }
    }
    internal class RemoveDishCommand : ICommand
    {
         Order order;
        Component dish;

        public RemoveDishCommand(Order order, Component dish)
        {
            this.order = order;
            this.dish = dish;
        }

        public void Execute()
        {
            order.dishes.Remove(dish);
            Console.WriteLine($"Removed {dish.GetTitle()} from the order.");
        }

        public void Undo()
        {
            order.dishes.Add(dish);
            Console.WriteLine($"Added {dish.GetTitle()} to the order.");
        }
    }
}
