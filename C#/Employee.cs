using Bogus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class Employee
    {
        static Faker faker = new Faker(locale:"fr");
        static Random Random = new Random();


        public bool gender { get; set; } // true - woman , false - man
        public UInt32 salary { get; set; }
        public string PIB { get; set; }
        public Employee(UInt32 min, UInt32 max)
        {
            if (Random.Next(0, 2) == 0)
            {
                gender = false;
                PIB = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
            }
            else
            {
                gender = true;
                PIB = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
            }
            salary = (uint)Random.Next((int)min, (int)max);
        }
    }
    internal class Unit
    {
        public static Random Random = new Random();
        public List<Employee> employees { get; set; }
        public string type { get; set; }
        internal UInt32 total_salary { get; set; }
        internal UInt16 count_people { get; set; }
        internal UInt16 count_mans { get; set; }
        internal UInt32 min_salary { get; set; }
        internal UInt32 max_salary { get; set; }


        public Unit(string type, uint min, uint max)
        {
            employees = new List<Employee>();
            this.type = type;
            this.min_salary = min;
            this.max_salary = max;
            Employee temp = null;
            for (int i = 0; i < Random.Next(3, 5); i++)
            {
                temp = new Employee(min, max);
                this.Add_Employee(temp);

            }


        }
        public void Add_Employee(Employee e)
        {
            employees.Add(e);
            total_salary += e.salary;
            count_people++;
            if (!e.gender)
            {
                count_mans++;
            }

        }
    }
    internal class department : IEnumerable<Unit>
    {
        List<Unit> units { get; set; }
        public UInt32 total_salaries { get; set; }
        public Double average_salary { get; set; }
        public UInt16 count_people { get; set; }
        public UInt16 count_mans { get; set; }
        public Double share_mans { get; set; }
        public UInt32 min_salary { get; set; }
        public UInt32 max_salary { get; set; }
        public IEnumerator<Unit> GetEnumerator()
        {
            return units.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return units.GetEnumerator();
        }
        public department()
        {
            units = null;
            total_salaries = 0;
            average_salary = 0;
            count_people = 0;
            count_mans = 0;
            share_mans = 0;
            min_salary = 0;
            max_salary = 0;

        }
        public void Add(Unit unit)
        {
            if (units == null)
            {
                units = new List<Unit>();
                min_salary = unit.min_salary;
                max_salary = unit.max_salary;
            }
            if (unit.max_salary > max_salary)
            {
                max_salary = unit.max_salary;
            }
            if (unit.min_salary < min_salary)
            {
                min_salary = unit.min_salary;
            }
            units.Add(unit);
            total_salaries += unit.total_salary;
            count_people += unit.count_people;
            count_mans += unit.count_mans;
            share_mans = (double)count_mans / count_people;
            average_salary = total_salaries / count_people;

        }
    }
    internal class Company : IEnumerable<department>
    {

        public List<department> departments { get; set; }
        static Random Random = new Random();
        public IEnumerator<department> GetEnumerator()
        {
            return departments.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return departments.GetEnumerator();
        }
        public Company()
        {
            departments = null;
        }
        public void Add()
        {
            if (departments == null)
            {
                departments = new List<department>();
            }
            department temp = new department();
            departments.Add(temp);
            for (int i = 0; i < departments.Count; i++)
            {
                temp.Add(new Unit("IT", (uint)Random.Next(1000, 10000), (uint)Random.Next(10000, 20000)));

            }
            foreach (var unit in temp)
            {
                for (int i = 0; i < departments.Count; i++)
                {
                    unit.Add_Employee(new Employee(unit.min_salary, unit.max_salary));
                    temp.count_people++;
                    if (!unit.employees.Last().gender)
                    {
                        temp.count_mans++;
                    }
                    temp.share_mans = (double)temp.count_mans / temp.count_people;
                }

            }
        }
    }
}