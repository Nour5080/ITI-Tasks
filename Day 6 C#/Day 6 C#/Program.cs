using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Day_6_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("==Hallo eng\\Maryam this is Nour's day 6 tasks==");
            Console.WriteLine("=================================================");
            while (true)
            {

                Console.WriteLine("==============Choose a Task to run===============");
                Console.WriteLine("1: OptimisedBubbleSort");
                Console.WriteLine("2: Range");
                Console.WriteLine("3:ArrayListReverser");
                Console.WriteLine("4: EvenNumbersFilter");
                Console.WriteLine("5: FixedSizeList");
                Console.WriteLine("6: FirstUniqueCharacter");
                Console.WriteLine("0: EXIT");
                Console.WriteLine("=================================================");

                string choise = Console.ReadLine();
                Console.Clear();

                switch (choise)
                {
                    case "1":
                        OptimisedBubbleSort.task1();
                        break;
                    case "2":
                        Task_Range.task2();
                        break;
                    case "3":
                        ArrayListReverser.task3();
                        break;
                    case "4":
                        EvenNumbersFilter.task4();
                        break;
                    case "5":
                        FixedSizeList.task5();
                        break;
                    case "6":
                        FirstUniqueCharacter.task6();
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