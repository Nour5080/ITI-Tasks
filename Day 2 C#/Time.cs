using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_C_
{
    struct time
    {
        public int hour;
        public int minute;
        public int sec;
    }
    internal class Time
    {
        public static void task3() {
                time t = new time();
            Console.WriteLine("Enter Time: ");
            Console.WriteLine("Hour: ");
            t.hour = int.Parse(Console.ReadLine());
            Console.WriteLine("Minute: ");
            t.minute = int.Parse(Console.ReadLine());
            Console.WriteLine("Second: ");
            t.sec = int.Parse(Console.ReadLine());
            Console.WriteLine($"Your Time is {t.hour}H:{t.minute}M:{t.sec}S");
        }
    }
}
