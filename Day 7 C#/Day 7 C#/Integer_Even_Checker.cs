using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_C_
{
    internal static class Integer_Even_Checker
    {
        public static bool IsEven(this int value)
        {
            if(value%2==0) return true;
            return false;
        }

        public static void task3()
        {
            Console.WriteLine("Enter The Numper: ");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine(num.IsEven());
        }
    }
}
