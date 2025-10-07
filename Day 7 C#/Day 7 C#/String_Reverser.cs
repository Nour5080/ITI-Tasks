using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_C_
{
    internal static class String_Reverser
    {
        public static string ReverseWord(this string input)
        {
            char[] chars = input.ToCharArray();

            int j = 0;
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                chars[j]=input[i];
                j++;
            }
            return new string(chars);
        }



        public static void task5()
        {
            Console.WriteLine("Enter Your Word: ");
            string Word = Console.ReadLine();
            Console.WriteLine(Word.ReverseWord());
        }
    }
}
