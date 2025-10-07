using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_C_
{
    internal static class DateTime_Age_Calculator
    {
        public static int CalcAge(this DateTime BirthDay)
        {
            DateTime today = DateTime.Today;
            int age = today.Year-BirthDay.Year;
            return age;
        }

        public static void task4()
        {
            Console.WriteLine("Enter Your Birth Day: ");
            Console.Write("Day: ");
            int d = int.Parse(Console.ReadLine());
            Console.Write("\nMonth: ");
            int m = int.Parse(Console.ReadLine());
            Console.Write("\nYear: ");
            int y = int.Parse(Console.ReadLine());
            DateTime BirthDay = new DateTime(y, m, d);
            Console.WriteLine($"\n\nYour Age is: {BirthDay.CalcAge()}");
        }
    }
}
