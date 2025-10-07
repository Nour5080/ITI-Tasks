using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_C_
{

    internal class ArrayListReverser
    {

        public static void SwapInArrayList(ArrayList list, int i, int j)
        {
            object temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void ReverseList(ArrayList list)
        {
            if (list is not null)
            {
                int left = 0, right = list.Count - 1;
                while (left < right)
                {
                    SwapInArrayList(list, left, right - left);
                    left++;
                    right--;
                }
            }
        }



public static void task3()
        {
            ArrayList list = new ArrayList() { 1, 2, 3, 4, 5, 6, 7, 8 };
            Console.WriteLine($"Before:");
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            } 
           

           ReverseList(list);
            Console.WriteLine("=================================================");

            Console.WriteLine($"After:");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            } 
        }
    }
}