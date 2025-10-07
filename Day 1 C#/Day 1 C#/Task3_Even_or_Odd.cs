using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task3_Even_or_Odd
    {
        public static void task3()
        {
            System.Console.Write("Enter your Numper: ");
            int Num = int.Parse(Console.ReadLine());
            if (Num % 2 == 0)
            {
                Console.Write("Your Numper is EVEN ");
            }
            else {
                Console.Write("Your Numper is ODD ");
            }
        }
    }
}
