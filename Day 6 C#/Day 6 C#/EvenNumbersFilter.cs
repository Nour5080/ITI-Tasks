using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_C_
{
    internal class EvenNumbersFilter
    {
        public static List<int> GetEvenNumpers(List<int> Numpers)
        {
            List<int> Evens = new List<int>();
            foreach (int n in Numpers)
            {
                if (n % 2 == 0) Evens.Add(n);
            }
                return Evens;
        }


        public static void task4()
        {
            List<int> Numpers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            List<int> Evens = new List<int>();
            Evens = GetEvenNumpers(Numpers);
           
            foreach (var item in Evens)
            {
                Console.WriteLine(item);
            }
        }
    }
}