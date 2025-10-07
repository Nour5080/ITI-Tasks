using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_Day_1
{
    internal class Querys1
    {
        public static void task1()
        {
            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };
            var orderedList = numbers.OrderBy(x => x).Distinct();
            foreach (int x in orderedList)
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("=================================================");
                                         
            foreach (int x in orderedList)
            {
                Console.WriteLine($"{{Numper = {x}, Multiply = {x*x}}}");
            }

            Console.WriteLine("=================================================");


        }
    }
}
