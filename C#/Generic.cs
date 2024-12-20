using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace Project2
{
    internal class Swap<T>
    {
        public static void swap(ref T value1, ref T value2)
        {
            T temp = value1;
            value1 = value2;
            value2 = temp;
            return;
        }
    }
    internal class Priority_queue<T> : IEnumerable<KeyValuePair<uint, T>>
    {
        SortedList<UInt32, T> queue;
        uint last_key;
        public Priority_queue(List<T> list)
        {

            queue = new SortedList<UInt32, T>();



            last_key = 0;


            foreach (var item in list)
            {
                queue.Add(last_key++, item);

            }
        }
        public Priority_queue(T item, uint priority)
        {
            queue = new SortedList<UInt32, T>();



            last_key = priority;


            queue.Add(priority, item);

        }
        public Priority_queue(T item)
        {
            queue = new SortedList<UInt32, T>();



            last_key = 0;
            queue.Add(0, item);
        }

        public T Pop()
        {
            if (queue.Count != 0)
            {
                KeyValuePair<uint, T> temp = queue.First();
                queue.RemoveAt(0);
                return temp.Value;
            }
            else
                return default;
        }
        public void Add(T item)
        {
            queue.Add(last_key++, item);
        }
        public void Remove(T item)
        {
            if (queue.ContainsValue(item))
            {


                queue.RemoveAt(queue.IndexOfValue(item));
                last_key--;
            }


        }
        public T PopLast()
        {
            if (queue.Count != 0)
            {
                KeyValuePair<uint, T> temp = queue.Last();
                queue.RemoveAt(queue.Count - 1);
                return temp.Value;
            }
            else
                return default;

        }
        IEnumerator<KeyValuePair<uint, T>> IEnumerable<KeyValuePair<uint, T>>.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() { return queue.GetEnumerator(); }
    }

    public class CircularQueue<T> : IEnumerable<T>
    {
        private T[] _buffer;
        private int _head;
        private int _tail;
        private int _size;

        public CircularQueue(int capacity)
        {
            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }
        public bool IsFull
        {
            get
            {
                return _size == _buffer.Length;
            }
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public void Enqueue(T item)
        {

            _buffer[_tail] = item;
            _tail = (_tail + 1) % _buffer.Length;
            if (_size < _buffer.Length)
            {
                _size++;
            }

        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Queue is empty.");
                return default;
            }
            T item = _buffer[_head];
            _buffer[_head] = default;
            _head = (_head + 1) % _buffer.Length;
            _size--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Queue is empty.");
                return default;
            }
            return _buffer[_head];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _buffer.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class Node<T>
    {
        public T Value;
        public Node<T> Next;
        public Node(T value)
        {
            Value = value;
            Next = null;
        }

    }
    public class DoublyLinkedNode<T>
    {
        public T Value { get; set; }
        public DoublyLinkedNode<T> Previous { get; set; }
        public DoublyLinkedNode<T> Next { get; set; }

        public DoublyLinkedNode(T value)
        {
            Value = value;
            Previous = null;
            Next = null;
        }
    }

    internal class list<T>
    {
        Node<T> head;
        Node<T> tail;
        uint _size;
        public list()
        {
            head = null;
            tail = null;
            _size = 0;
        }
        public void Add(T item)
        {
            if (_size == 0)
            {
                head = new Node<T>(item);
                tail = head;
                _size++;
                return;
            }
            else
            {
                tail.Next = new Node<T>(item);
                tail = tail.Next;
                _size++;
                return;
            }

        }
        public T Pop()
        {
            if (_size == 0)
            {
                return default;
            }
            else
            {
                if (head.Next == null)
                {
                    T temp = head.Value;
                    head = null;
                    tail = null;
                    _size--;
                    return temp;
                }
                else
                {
                    T temp = head.Value;
                    head = head.Next;
                    _size--;
                    return temp;

                }

            }

        }
        public Node<T> this[uint index]
        {
            get
            {
                Node<T> temp = null;
                if (index < _size)
                {
                    temp = head;
                    for (int i = 0; i < index; i++)
                    {
                        temp = temp.Next;

                    }
                    return temp;
                }
                else
                {
                    return temp;
                }
            }
            set
            {
                Node<T> temp = null;
                if (index >= 0 && index < _size)
                {
                    temp = head;
                    for (int i = 0; i < _size; i++)
                    {
                        temp = temp.Next;
                        if (i == index)
                        {
                            temp.Value = value.Value;
                            break;
                        }
                    }
                }

            }

        }
        public T DeleteLast()
        {
            if (tail == null)
            {
                return default;
            }
            else
            {
                if (_size != 1)
                {
                    T temp = tail.Value;
                    tail = this[_size - 2];
                    tail.Next = null;
                    _size--;
                    return temp;

                }
                else
                {
                    T temp = head.Value;
                    head = null;
                    tail = null;
                    _size = 0;
                    return temp;
                }
            }

        }



    }
    public class DoublyLinkedList<T>
    {
        private DoublyLinkedNode<T> head;
        private DoublyLinkedNode<T> tail;
        private int _size;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            _size = 0;
        }

        
        public void AddLast(T value)
        {
            DoublyLinkedNode<T> newNode = new DoublyLinkedNode<T>(value);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            _size++;
        }

        
        public void AddFirst(T value)
        {
            DoublyLinkedNode<T> newNode = new DoublyLinkedNode<T>(value);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            _size++;
        }

        
        public T RemoveFirst()
        {
            if (head == null)
                return default;

            T value = head.Value;
            if (head.Next != null)
            {
                head = head.Next;
                head.Previous = null;
            }
            else
            {
                head = null;
                tail = null;
            }
            _size--;
            return value;
        }

        
        public T DeleteLast()
        {
            if (tail == null)
                return default;

            T value = tail.Value;
            if (tail.Previous != null)
            {
                tail = tail.Previous;
                tail.Next = null;
            }
            else
            {
                head = null;
                tail = null;
            }
            _size--;
            return value;
        }

        
        public bool Contains(T value)
        {
            DoublyLinkedNode<T> temp = head;
            while (temp != null)
            {
                if (temp.Value.Equals(value))
                    return true;
                temp = temp.Next;
            }
            return false;
        }

        
        public void See()
        {
            DoublyLinkedNode<T> temp = head;
            while (temp != null)
            {
                Console.WriteLine(temp.Value);
                temp = temp.Next;
            }
        }

        
        public int size
        {
            get { return _size; }
        }
    }


}
