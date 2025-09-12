using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_C_
{
    internal class Task5_Student_Grade_Calculator
    {
        public static void task5()
        {
            Console.WriteLine("Enter the total degree ");
            int Tdegree = int.Parse(Console.ReadLine());
            Console.Write("Enter Your Degree: ");
            int degree = int.Parse(Console.ReadLine());
            float grade = degree / Tdegree;
            if (grade >=0.90 ) Console.WriteLine("Your Grade is: A ");
            else if (grade >=0.85&& degree <0.90 ) Console.WriteLine("Your Grade is: A- ");
            else if (grade >=0.80&& degree <0.85 ) Console.WriteLine("Your Grade is: B+ ");
            else if (grade >=0.75&& degree <0.80 ) Console.WriteLine("Your Grade is: B ");
            else if (grade >=0.70&& degree <0.75 ) Console.WriteLine("Your Grade is: C+ ");
            else if (grade >=0.65&& degree <0.70 ) Console.WriteLine("Your Grade is: C ");
            else if (grade >=0.60&& degree <0.65 ) Console.WriteLine("Your Grade is: D ");
            else if (grade < 0.60) Console.WriteLine("Your Grade is: F ");

            Console.WriteLine("Standby this Grade Table");
            Console.WriteLine("|-----------------|");
            Console.WriteLine("| A  | \">90%\"     |");
            Console.WriteLine("| A- | \"85%:<90%\" |");
            Console.WriteLine("| B+ | \"80%:<85%\" |");
            Console.WriteLine("| B  | \"75%:<80%\" |");
            Console.WriteLine("| C+ | \"70%:<75%\" |");
            Console.WriteLine("| C  | \"65%:<70%\" |");
            Console.WriteLine("| D  | \"60%:<65%\" |");
            Console.WriteLine("| F  | \"<60%\"     |");
            Console.WriteLine("|-----------------|");
        }
    }
}
