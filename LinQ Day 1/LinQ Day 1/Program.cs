
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LinQ_Day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("======Hallo eng\\Maryam this is Nour's tasks=====");
            Console.WriteLine("=================================================");
            while (true)
            {

                Console.WriteLine("==============Choose a Task to run===============");
                Console.WriteLine("1: Querys1");
                Console.WriteLine("2: Querys2");
                Console.WriteLine("3: Querys3");
                Console.WriteLine("0: EXIT");
                Console.WriteLine("=================================================");

                string choise = Console.ReadLine();
                Console.Clear();

                switch (choise)
                {
                    case "1":
                        Querys1.task1();
                        break;
                    case "2":
                        Querys2.task2();
                        break;
                    case "3":
                        Querys3.task3();
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
