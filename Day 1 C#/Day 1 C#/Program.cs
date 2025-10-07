using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Day_1_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("==Hallo eng\\Maryam this is Nour's day one tasks==");
            Console.WriteLine("=================================================");
            while (true)
            {
                
                Console.WriteLine("==============Choose a Task to run===============");
                Console.WriteLine("1: Character to ASCII");
                Console.WriteLine("2: ASCII to Character");
                Console.WriteLine("3: Even or Odd");
                Console.WriteLine("4: Simple Calculator");
                Console.WriteLine("5: Student Grade Calculator");
                Console.WriteLine("6: Multiplication Table");
                Console.WriteLine("0: EXIT");
                Console.WriteLine("=================================================");

                string choise = Console.ReadLine();
                Console.Clear();

                switch (choise) {
                    case "1":
                        Task1_Character_to_ASCII.task1();
                        break;
                    case "2":
                        Task2_ASCII_to_Character.task2();
                        break;
                    case "3":
                        Task3_Even_or_Odd.task3();
                        break;
                    case "4":
                        Task4_Simple_Calculators.task4();
                        break;
                    case "5":
                        Task5_Student_Grade_Calculator.task5();
                        break;
                    case "6":
                        Task6_Multiplication_Table.task6();
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