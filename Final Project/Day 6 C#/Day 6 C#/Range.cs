using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day_6_C_.Task_Range;

namespace Day_6_C_
{
    internal class Task_Range
    {
        public class Range<T> where T : IComparable<T>
        {
            public T Max { get; set; }
           
            public T Min { get; set; }

            public Range(T min,T max) {
                Max = max; 
                Min = min;
            }

            public T Length()
            {
                return (dynamic)Max - (dynamic)Min;
            }


            public bool IsInRange(T obj)  
            {
                if(obj == null) return false;

                return obj.CompareTo(Min) >= 0 && obj.CompareTo(Max) <= 0;
            }

            public override string ToString()
            {
                return $"Range: [{Min} - {Max}]";
            }

        }
        public static void task2()
        {
         
               
                var intRange = new Range<int>(10, 20);
                Console.WriteLine(intRange);
                Console.WriteLine("Length = " + intRange.Length());
                Console.WriteLine("Is 15 in range? " + intRange.IsInRange(15));
                Console.WriteLine("Is 25 in range? " + intRange.IsInRange(25));
                Console.WriteLine();

                var doubleRange = new Range<double>(5.5, 9.9);
                Console.WriteLine(doubleRange);
                Console.WriteLine("Length = " + doubleRange.Length());
                Console.WriteLine("Is 7.2 in range? " + doubleRange.IsInRange(7.2));
                Console.WriteLine("Is 10 in range? " + doubleRange.IsInRange(10));
                Console.WriteLine();

               
                var stringRange = new Range<string>("Ali", "Omar");
                Console.WriteLine(stringRange);
                Console.WriteLine("Is 'Hana' in range? " + stringRange.IsInRange("Hana"));
                Console.WriteLine("Is 'Ziad' in range? " + stringRange.IsInRange("Ziad"));

                Console.ReadKey();
        }
    }
}

