using Bogus;
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
            IFactory factory = new StudentFactory();
            Student student = (Student)factory.Create();
            factory = new TeacherFactory();
            Teacher teacher = (Teacher)factory.Create();
            factory = new CourseFactory();
            Course course = (Course)factory.Create();
            student.AddCourse(course);
            teacher.AddCourse(course);
            factory = new StudentFactory();
            student = (Student)factory.Create();
            student.AddCourse(course);
        }
    }

    interface IFactory
    {
        Education Create();
    }
    class StudentFactory : IFactory
    {
        public Education Create()
        {
            return new Student();
        }
    }
    class TeacherFactory : IFactory
    {
        public Education Create()
        {
            return new Teacher();
        }
    }
    class CourseFactory : IFactory
    {
        public Education Create()
        {
            return new Course();
        }
    }
    abstract class Education
    {
        protected string Name { get; set; }
        protected uint Id { get; set; }

    }
    class Student : Education
    {

        static Faker faker = new Faker();

        
        List<Course> Courses { get; set; }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
            course.AddStudent(this);
        }
        public Student()
        {
            this.Name = faker.Name.FullName();
            this.Id = (uint)faker.Random.UInt();
            this.Courses = new List<Course>();


        }

    }
    class Teacher : Education
    {
        static Faker faker = new Faker();

        




        List<Course> Courses { get; set; }
        ushort experience { get; set; }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
            course.AddTeacher(this);
        }
        public Teacher()
        {
            this.Name = faker.Name.FullName();
            this.Id = (uint)faker.Random.UInt();
            this.experience = faker.Random.UShort(1, 100);
            this.Courses = new List<Course>();


        }
    }
    class Course : Education
    {
        static Faker faker = new Faker();
        

        Teacher teacher { get; set; }
        List<Student> Students { get; set; }
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
        public void AddTeacher(Teacher teacher)
        {
            this.teacher = teacher;
        }
        public Course()
        {
            this.Name = faker.Company.CompanyName();
            this.Id = (uint)faker.Random.UInt();
            this.Students = new List<Student>();
            this.teacher = null;
        }
    }
}
