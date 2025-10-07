using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task6_Multiplication_Table
    {
        public static void task6()
        {
            Console.WriteLine("Enter Numper of The table)");
            int table_num = int.Parse(Console.ReadLine());
            for (int i = 0; i <= 10; i++) {
                Console.WriteLine($"{table_num} * {i} = {table_num*i}");
            }
        }
    }
}
