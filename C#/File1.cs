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
            List<Car> cars = new List<Car>();
            for (int i = 0; i < 2; i++)
            {
                cars.Add(new Sport_Car());
            }
            for (int i = 0;i < 2; i++)
            {
                cars.Add(new HatchBack_Car());
            }
            Racing racing = new Racing(cars);
            racing.Start(1000);

        }
        
    }
}
