using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_C_
{
    internal class  FirstUniqueCharacter
    {
            public static int FirstNonRepeatedChar(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (freq.ContainsKey(c))
                    freq[c]++;
                else
                    freq[c] = 1;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (freq[s[i]] == 1)
                    return i;
            }
            return -1;
        }
    
        public static void task6()
        {
            Console.WriteLine("Enter the string: ");
            string the_string = Console.ReadLine();
            int index=FirstNonRepeatedChar(the_string);
            Console.WriteLine($"First non-repeated char index in '{the_string}': {index}");
        }
    }
}