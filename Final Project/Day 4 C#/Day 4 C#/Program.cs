using Day_4_C_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
namespace Day_4_C_
{
    internal class Program
    {
        class Company
        {
            public string Name { get; set; }
            public List<Department> Departments { get; set; } = new List<Department>();
        }
        class Department
        {
            public string Name { get; set; }
            public List<Employee> Employees { get; set; } = new List<Employee>();
        }
        class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Position { get; set; }
            public List<Course> Courses { get; set; } = new List<Course>();
        }

        enum CourseLevel { Beginner, Intermediate, Advanced }

        class Course
        {
            public string Name { get; set; }
            public Instructor Instructor { get; set; }
            public List<Student> Students { get; set; }
            public CourseLevel Level { get; set; }

            public Course(string name)
            {
                Name = name;
                Students = new List<Student>();
            }
        }

        class Engine
        {
            public string Model { get; set; }
            public int HorsePower { get; set; }
            public void Start()
            {
                Console.WriteLine("Engine started");
            }
        }
        class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public Engine Engine = new Engine();
            public void Start() => Engine.Start();

        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int ID { get; set; }
            public Person(string name, int age)
            {
                this.Name = name;
                this.Age = age;
                ID = IdGenerator.GetNext();
            }

            public virtual void Introduce()
            {
                Console.WriteLine("Hi,I'm a Person");
            }
        }

        class Instructor : Person
        {
            public Instructor(string name, int age) : base(name, age) { }
            public List<Course> Ins_Courses { get; set; } = new List<Course>();

            public void TeachCourse(Course Ins_course)
            {
                Ins_Courses.Add(Ins_course);
                Ins_course.Instructor = this;
            }

            public override void Introduce()
            {
                Console.WriteLine("Hi,I'm a Instructor");
            }

        }

        class Grade
        {
            public int Value { get; set; }
            public Grade(int v)
            {
                Value = v;
            }
            public static Grade operator +(Grade a, Grade b) => new Grade(a.Value + b.Value);
            public static bool operator ==(Grade a, Grade b) => a.Value == b.Value;
            public static bool operator !=(Grade a, Grade b) => a.Value != b.Value;
        }

        class Student :Person
        {
            public Student(string name,int age) :base(name, age) { }
            public List<Course> Std_Courses { get; set; } = new List<Course>();
            public List<Grade> grades { get; set; } = new List<Grade>() ;

            public void RegisterCourse(Course Std_course)
            {
                Std_Courses.Add(Std_course);
                Std_course.Students.Add(this);

                switch (Std_course.Level)
                {
                    case CourseLevel.Beginner:
                        Console.WriteLine($"{Name}  registered in {Std_course.Name}: Good luck starting out! ");
                        break;
                    case CourseLevel.Intermediate:
                        Console.WriteLine($"{Name}  registered in {Std_course.Name}: Keep going!");
                        break;
                    case CourseLevel.Advanced:
                        Console.WriteLine($"{Name}  registered in {Std_course.Name}: This will be challenging!");
                        break;
                }
            }

            public void AddGrade(int value)
            {
                grades.Add(new Grade(value));
            }
            
            public Grade  TotalGreades()
            {
                Grade Total = new Grade(0);
                foreach (var item in grades)
                {
                    Total += item;
                }
                return Total;
            }

            public override void Introduce()
            {
                Console.WriteLine("Hi,I'm a Student");
            }

        }

        static class IdGenerator
        {
            public static int CurrentId = 0;
            public static int GetNext() => CurrentId++;
        }
        public interface IDrawable
                {
                    void  Draw();
                }
        public abstract class Shape : IDrawable
        {
            public abstract double Area();
            public abstract void Draw();
        }


        public class Circle : Shape
        {
            public double radius { get; set; }

            public Circle(double r) {
                radius = r;
            }

            public override double Area() {
                return Math.PI * radius * radius;
            }

            public override void Draw()
            {
                Console.WriteLine("Drawing Circle o");
            }

        }

        public class Rectangle : Shape
        {
            public double width { get; set; }
            public double height { get; set; }

            public Rectangle(double w, double h)
            {
                width = w;
                height = h;
            }

            public override double Area()
            {
                return width * height;
            }

            public override void Draw()
            {
                Console.WriteLine("Drawing Rectangle []");
            }
        }




        static void Main(string[] args)
        {
            Student s1 = new Student("Mona", 20);
            Student s2 = new Student("Ali", 22);
            Student s3 = new Student("Sara", 21);
            Student s4 = new Student("Omar", 23);
            Instructor inst1 = new Instructor("Dr. Smith", 45);
            Instructor inst2 = new Instructor("Prof. Johnson", 50);
            Instructor inst3 = new Instructor("Ms. Lee", 35);
            s1.AddGrade(85);
            s1.AddGrade(90);
            s2.AddGrade(78);
            s3.AddGrade(92);
            s1.AddGrade(88);
            s4.AddGrade(76);
            s4.AddGrade(80);
            Course c1 = new Course("C# Basics");
            Course c2 = new Course("Databases");
            Course c3 = new Course("Web Development");
            Course c4 = new Course("Advanced C#");
            Course c5 = new Course("Data Science");
            c1.Level = CourseLevel.Beginner;
            c2.Level = CourseLevel.Intermediate;
            c3.Level = CourseLevel.Beginner;
            c4.Level = CourseLevel.Advanced;
            c5.Level = CourseLevel.Advanced;

            s1.RegisterCourse(c1);
            s1.RegisterCourse(c4);
            s2.RegisterCourse(c2);
            s3.RegisterCourse(c3);
            s4.RegisterCourse(c5);
            s4.RegisterCourse(c1);

            inst1.TeachCourse(c1);
            inst1.TeachCourse(c2);
            inst3.TeachCourse(c4);
            inst2.TeachCourse(c5);
            inst2.TeachCourse(c3);



            s1.Introduce();
            s2.Introduce();
            s3.Introduce();
            s4.Introduce();

            inst1.Introduce();
            inst2.Introduce();
            inst3.Introduce();

            Company company = new Company { Name = "TechCorp" };
            Department devDept = new Department { Name = "Development" };
            Department hrDept = new Department { Name = "HR" };
            company.Departments.Add(devDept);
            company.Departments.Add(hrDept);

            Employee emp1 = new Employee { Name = "Alice", Age = 28, Position = "Developer" };
            Employee emp2 = new Employee { Name = "Bob", Age = 32, Position = "HR Manager" };
            devDept.Employees.Add(emp1);
            hrDept.Employees.Add(emp2);
            Console.WriteLine("\nDepartments:");
            foreach (var dept in company.Departments)
            {
                Console.WriteLine($"Department: {dept.Name}, Employees: {dept.Employees.Count}");
            }

            Console.WriteLine("\nShapes:");

            List<Shape> shapes = new List<Shape>
            {
                new Circle(5),
                new Rectangle(4, 6)
            };

            Console.WriteLine("Shapes info:\n");
            foreach (Shape shape in shapes)
            {
                Console.WriteLine($"Area: {shape.Area()}");

                
                (shape as IDrawable)?.Draw();
                Console.WriteLine();
            }
        }
    }
}
