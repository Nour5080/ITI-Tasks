using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_C_
{
    internal class Store_and_Print_Student_Names
    {
        public static void task1()
        {
            Console.WriteLine("Enter the Numper of Student: ");
            int Num = int.Parse(Console.ReadLine());
            string[] Std_Names = new string[Num];
            Console.WriteLine("Enter the Names of Students: ");
            for (int i = 0; i < Num; i++)
            {
                Std_Names[i] = Console.ReadLine();
            }
            Console.WriteLine(" ");
            Console.WriteLine("the Names of Students: ");
            for (int i = 0;i < Num; i++)
            {
                Console.WriteLine($"{i+1}- {Std_Names[i]}.");
            }
        }
    }
}
