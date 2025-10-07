using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Day_7_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("==Hallo eng\\Maryam this is Nour's day 7 tasks==");
            Console.WriteLine("=================================================");
            while (true)
            {

                Console.WriteLine("==============Choose a Task to run===============");
                Console.WriteLine("1: Anonymous Object Creation");
                Console.WriteLine("2: String Word Counter");
                Console.WriteLine("3: Integer Even Checker");
                Console.WriteLine("4: DateTime Age Calculator");
                Console.WriteLine("5: String Reverser");
                Console.WriteLine("0: EXIT");
                Console.WriteLine("=================================================");

                string choise = Console.ReadLine();
                Console.Clear();

                switch (choise)
                {
                    case "1":
                        Anonymous_Object_Creation.task1();
                        break;
                    case "2":
                        String_Word_Counter.task2();
                        break;
                    case "3":
                        Integer_Even_Checker.task3();
                        break;
                    case "4":
                        DateTime_Age_Calculator.task4();
                        break;
                    case "5":
                        String_Reverser.task5();
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