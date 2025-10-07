using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinQ_Day_1.Querys3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LinQ_Day_1
{
    internal class Querys3
    {
        public class Subject
        {
            public int Code {  get; set; }
            public string Name { get; set; }
           
            public Subject() {
                Code = 0;
                Name = "";
             }
        }

        public class Student
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Subject[] subjects { get; set; }
        }


        public static void task3()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){ ID=1, FirstName="Ali", LastName="Mohammed",
                subjects=new Subject[]{ new Subject(){ Code=22,Name="EF"}, new Subject(){
                Code=33,Name="UML"}}},
                new Student(){ ID=2, FirstName="Mona", LastName="Gala",
                subjects=new Subject []{ new Subject(){ Code=22,Name="EF"}, new Subject (){
                Code=34,Name="XML"},new Subject (){ Code=25, Name="JS"}}}, new
                Student(){ ID=3, FirstName="Yara", LastName="Yousf", subjects=new Subject
                []{ new Subject (){ Code=22,Name="EF"}, new Subject (){
                Code=25,Name="JS"}}},
                new Student(){ ID=1, FirstName="Ali", LastName="Ali",
                subjects=new Subject []{ new Subject (){ Code=33,Name="UML"}}},
            };

            var AnonymousQuery = students.Select(x => new {FullName=x.FirstName+" "+x.LastName
                                                            , NoOFsubjects=x.subjects.Length });
            
            foreach (var name in AnonymousQuery)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("=================================================");

            var OrderDescendingNames = students.OrderByDescending(x => x.FirstName)
                                               .ThenBy(x=>x.LastName)
                                               .Select(x => x.FirstName + " " + x.LastName);
            foreach (var name in OrderDescendingNames)
            {
                Console.WriteLine(name);
            }
            
            Console.WriteLine("=================================================");

            var StudentSubjects = students.SelectMany(s => s.subjects.Select(sub => new
            {
                StudentName = s.FirstName + " " + s.LastName,
                SubjectName = sub.Name
            }));

            foreach (var name in StudentSubjects)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("=================================================");

            var Bounse = students.SelectMany(s => s.subjects.Select(
                sub => Tuple.Create(s.FirstName + " " + s.LastName,sub.Name)))
                .GroupBy(s => s.Item1);

            foreach (var name in Bounse)
            {
                Console.WriteLine(name.Key);

                foreach (var subject in name)
                {
                    Console.WriteLine(subject.Item2);
                }
            }

        }    
    }
}
