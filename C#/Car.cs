using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    internal abstract class Car
    {
        protected static Random random = new Random();
        protected static Faker faker = new Faker();
        protected ushort Speed;
        protected ushort weight;
        protected string color;
        public string driver;
        protected float percent_speed;
        protected float percent_drived;
        protected Int32 length_track;
        public Boolean IsFinished;
        public string track;
        protected event Action finished;

        public void Start(Int32 length)
        {
            IsFinished = false;
            this.length_track = length;
            percent_speed = (float)Speed / length_track;
            percent_drived = 0;

        }
        public virtual void Update_Speed()
        {
            if (length_track != 0 && IsFinished != true)
            {
                Speed = (ushort)random.Next(10, 100); // M/S 
                percent_speed = (float)Speed / length_track;
                return;
            }
            return;
        }
        public void Update_info()
        {
            percent_drived += percent_speed;
            for (int i = 0; i < (int)(percent_speed * 50); i++)
            {
                track += '*';
            }
            if (track.Length >= 50) {
                IsFinished = true;
               
                finished();
            }
            

        }
        protected virtual void Finishing()
        {
            Console.WriteLine($"{this.driver} finished!");
            return;
        }
    }
    internal class Sport_Car : Car
    {
        
        

        public Sport_Car()
        {
            Speed = (ushort)random.Next(10, 100); // M/S
            weight = (ushort)random.Next(10, 30); //KG
            color = faker.Commerce.Color();
            driver = faker.Name.FullName();
            length_track = 0;
            percent_drived = 0;
            percent_speed = 0;
            track = "";
            finished += new Action(Finishing);
            

        }

        public override void Update_Speed()
        {
            if (length_track != 0 && IsFinished != true)
            {
                Speed = (ushort)random.Next(75, 100); // M/S 
                percent_speed = (float)Speed / length_track;
                return;
            }
            return;
        }
        protected override void Finishing()
        {
            Console.WriteLine($"{this.driver} on {this.color} sport car finished");
        }



    }
    internal class Racing
    {
        public List<Car> cars;
        protected event Action Updating_Info;
        protected event Action Updating_Speed;
        protected Car winner;
        public Racing(List<Car> cars)
        {
            this.cars = cars;
            foreach (var item in this.cars)
            {
                Updating_Info += item.Update_info;
                Updating_Speed += item.Update_Speed;
            }
            winner = null;
        }
        public Racing()
        {
            this.cars = new List<Car>();
            winner = null;
        }
        public void Add(Car car)
        {
            this.cars.Add(car);
            Updating_Info += car.Update_info;
            Updating_Speed += car.Update_Speed;
        }
        void PaintTrack()
        {
            foreach (var item in this.cars)
            {
                Console.WriteLine($"--------------------------------------------------  {item.driver} ");
                Console.WriteLine(item.track);
                Console.WriteLine($"-------------------------------------------------- ");
            }
        }
        public void Start(Int32 track) // Meters
        {
            foreach (var item in this.cars)
            {
                item.Start(track);

            }
            while (true && cars.Count != 0)
            {
                Updating_Info();
                Updating_Speed();
                
                winner = cars.Find(Finish_Predicate);
                PaintTrack();
                if (winner != null)
                {
                    
                    Console.WriteLine(winner.track.Length);
                    Console.WriteLine($"{winner.driver} is winner!");
                    break;
                }
                Thread.Sleep(1000);
               
            }
            Console.WriteLine("Racing will reboot");
            //cars.Clear();
        }
        Boolean Finish_Predicate(Car car)
        {
            return car.IsFinished;
        }
    }
}
