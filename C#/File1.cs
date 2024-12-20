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
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < 10; i++) { 
                if(i%2 != 0)
                {
                    list.Add(i);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    list2.Add(i);
                }
            }
            Swap<List<int>>.swap(ref list, ref list2);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("-----------");
            foreach (int i in list2)
            {
                Console.WriteLine(i);
            }
            Priority_queue<int> _Queue = new Priority_queue<int>(list);
            Console.WriteLine(_Queue.PopLast());
            Console.WriteLine("------");
            _Queue.Remove(2);
            foreach(var item in _Queue)
            {
                Console.WriteLine(item.Value);
            }
            Console.WriteLine("------");
            CircularQueue<int> _Circular = new CircularQueue<int>(10);
            for (int i = 0;!_Circular.IsFull ; i++)
            {
                _Circular.Enqueue(i);
            }
            for (int i = 0; i < 2; i++)
            {
                _Circular.Enqueue(10);
            }
            for (int i = 0; i < 2; i++)
            {
                _Circular.Dequeue();
            }
            list<string> l1 = new list<string>();
            l1.Add("Hello");
            l1.Add("World");
            Console.WriteLine(l1[0].Value + l1[1].Value);
            l1.Pop();
            Console.WriteLine(l1[0].Value);
            DoublyLinkedList<string> doublyLinkedList = new DoublyLinkedList<string>();
            doublyLinkedList.AddFirst("Hello");
            doublyLinkedList.AddLast("World");
           
            Console.WriteLine(doublyLinkedList.DeleteLast());
        }
        
    }
}
