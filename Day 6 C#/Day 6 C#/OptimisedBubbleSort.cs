using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_C_
{
    internal class OptimisedBubbleSort
    {
       public class Helper 
        {

            public static void SWAP<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }

            public static void BubbleSort<T>(T[] Array) where T : IComparable<T>
            {
                if (Array is not null)
                {
                    for (int i = 0; i < Array.Length; i++)
                    {
                        for (int j = 0; j < Array.Length - i - 1; j++)
                        {
                            if (Array[j].CompareTo(Array[j + 1]) == 1)
                            {
                                SWAP(ref Array[j], ref Array[j + 1]);
                            }
                        }
                    }
                }
            }
        
        }


            public static void task1()
            {
            int[] numbers = { 5, 3, 8, 1, 2 };

            Console.WriteLine("=================================================");

            Console.WriteLine($"Before:");
            Console.WriteLine();
            foreach (var item in numbers)
                Console.WriteLine(item);

            Helper.BubbleSort(numbers);

            Console.WriteLine("=================================================");

            Console.WriteLine($"After:");
            Console.WriteLine();
            foreach (var item in numbers)
                Console.WriteLine(item);


            string[] names = { "Nour", "Ali", "Hana", "Omar" };


            Console.WriteLine("=================================================");

            Console.WriteLine($"Before:");
            Console.WriteLine();
            foreach (var item in names)
                Console.WriteLine(item);

            Helper.BubbleSort(names);

            Console.WriteLine("=================================================");

            Console.WriteLine($"After:");
            Console.WriteLine();
            foreach (var item in names)
                Console.WriteLine(item);




        }

    }
    }

