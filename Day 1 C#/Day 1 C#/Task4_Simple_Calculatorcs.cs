using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task4_Simple_Calculators
    {
        public static void task4()
        {
            Console.Write("Enter Your First Numper: ");
            int Num1 = int.Parse(Console.ReadLine());
            Console.Write("Enter Your Second Numper: ");
            int Num2 = int.Parse(Console.ReadLine());
            Console.Write($"{Num1} + {Num2} = {Num1+Num2}");
            Console.Write($"{Num1} - {Num2} = {Num1-Num2}");
            Console.Write($"{Num1} * {Num2} = {Num1*Num2}");
        }
    }
}
