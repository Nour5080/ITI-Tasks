using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
namespace Day_2_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("==Hallo eng\\Maryam this is Nour's day Two tasks==");
            Console.WriteLine("=================================================");
            while (true)
            {

                Console.WriteLine("==============Choose a Task to run===============");
                Console.WriteLine("1: Store and Print Student Names");
                Console.WriteLine("2: Student Ages by Track & Average");
                Console.WriteLine("3: Time");
                Console.WriteLine("0: EXIT");
                Console.WriteLine("=================================================");

                string choise = Console.ReadLine();
                Console.Clear();

                switch (choise)
                {
                    case "1":
                        Store_and_Print_Student_Names.task1();
                        break;
                    case "2":
                        Student_Ages_by_Track_Average.task2();
                        break;
                    case "3":
                        Time.task3();
                        break;
                    case "0":
                        Console.WriteLine("see you");
                        return;
                    default:
                        Console.WriteLine("invalid choise...try again   :)");
                        break;
                }

                Console.WriteLine("press any key to return to the menu");
                Console.ReadKey();
                System.Console.WriteLine(" ");
            }
        }
    }
}