using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task1_Character_to_ASCII
    {
        public static void task1() {

            System.Console.WriteLine("Enter  the Character: ");
            char Character = Console.ReadKey().KeyChar;
            System.Console.WriteLine(" ");
            int ASCII = (int)Character;
            System.Console.WriteLine(ASCII);

        }
    }
}
