using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_C_
{
    internal static class String_Word_Counter
    {
      public static int CountWords(this string sentence)
        { 
            var splitArray = sentence.Split(' ');
            return splitArray.Count();
        }
        public static void task2()
        {
            Console.WriteLine("Enter The sentence: ");
            string sen = Console.ReadLine();
            Console.WriteLine("THE Numper of words: ");
            Console.WriteLine(CountWords(sen));


        }
    }
}
