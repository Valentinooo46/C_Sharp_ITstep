using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Employee
    {
        string PIB;
        string Date_Birthday;
        string Fax;
        string email;
        string role;
        string work_duty;
        Double salary;
        public Employee(string pIB, string date_Birthday, string fax, string email, string role, string work_duty, Double salary)
        {
            PIB = pIB;
            Date_Birthday = date_Birthday;
            Fax = fax;
            this.email = email;
            this.role = role;
            this.work_duty = work_duty;
            this.salary = salary;
        }
        public string GetPIB
        {
            get
            {
                return PIB;
            }
        }
        public static Double operator +(Employee ee, Double add)
        {
            return ee.salary + add;
        }
        public static Double operator -(Employee ee, Double add)
        {
            return (ee.salary < add) ? (0) : (ee.salary - add);
        }
        public static bool operator ==(Employee ee1, Employee ee2)
        {
            return (ee1.salary == ee2.salary) ? (true) : (false);
        }
        public static bool operator !=(Employee ee1, Employee ee2)
        {
            return (ee1.salary != ee2.salary) ? (true) : (false);
        }
        public static bool operator <(Employee ee1, Employee ee2)
        {
            return (ee1.salary < ee2.salary) ? (true) : (false);
        }
        public static bool operator >(Employee ee1, Employee ee2)
        {
            return (ee1.salary > ee2.salary) ? (true) : (false);
        }
        public string GetDate_Birthday
        {
            get
            {
                return Date_Birthday;
            }
        }
        public Double GetSalary
        {
            get
            {
                return salary;
            }
            set
            {
                
                salary = value;
            }
        }
        public string GetFax
        {
            get
            {
                return Fax;
            }
        }
        public string GetEmail
        {
            get
            {
                return email;
            }
        }
        public string Getrole
        {
            get
            {
                return role;
            }
        }
        public string Getwork_duty
        {
            get
            {
                return work_duty;
            }
        }
        public ref string GetSetPIB()
        {
            return ref PIB;
        }
        public ref string GetSetDate_Birthday()
        {
            return ref Date_Birthday;
        }
        public ref string GetSetFax()
        {
            return ref Fax;
        }
        public ref string GetSetEmail()
        {
            return ref email;
        }
        public ref string GetSetRole()
        {
            return ref role;
        }
        public ref string GetSetWork_duty()
        {
            return ref work_duty;
        }
        public override string ToString()
        {
            return "PIB:\n" + PIB + "\nDate Birtaday:\n" + Date_Birthday + "\nFax:\n" + Fax + "\nemail:\n" + email + "\nrole:\n" + role + "\nwork_duty:\n" + work_duty;
        }
    }
    internal class File3
    {
        public static void Main(string[] args)
        {
            Employee employee = new Employee("SVA", "01.09.2009", "+380993710492", "example@gmail.com", "Enginner", "Repair electronic systems", 20000);
            employee.GetSalary = employee - 20000;
            Console.WriteLine(employee.GetSalary);
            Employee employee2 = new Employee("SVA", "01.09.2009", "+380993710492", "example@gmail.com", "Enginner", "Repair electronic systems", 0);
            if (employee2 == employee)
            {
                Console.WriteLine("Salaries are equal");

            }
            else
            {
                Console.WriteLine("Salaries aren't equal");
            }
            employee2.GetSalary = 20000;
            if (employee2 == employee)
            {
                Console.WriteLine("Salaries are equal");

            }
            else
            {
                Console.WriteLine("Salaries aren't equal");
            }
        }
    }
}
