using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task2_ASCII_to_Character
    {
        public static void task2()
        {
            System.Console.WriteLine("Enter  the ASCII: ");
            int ASCII = int.Parse(Console.ReadLine());
            System.Console.WriteLine(" ");
            char Character = (char)ASCII;
            System.Console.WriteLine(Character);

        }
    }
}
