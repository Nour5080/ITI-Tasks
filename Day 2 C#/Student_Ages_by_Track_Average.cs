using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_C_
{
    internal class Student_Ages_by_Track_Average
    {
        public static void task2()
        { 
            Console.WriteLine("Enter the Numper of Tracks: ");
            int Num_Tracks = int.Parse(Console.ReadLine());

            int[][] Tracks= new int [Num_Tracks][];
            for (int i = 0; i < Num_Tracks; i++)
            {
                Console.WriteLine($"Enter the Numper of Student in the track {i + 1}: ");
                int Num_Std = int.Parse(Console.ReadLine());
                Tracks[i] = new int[Num_Std];

                Console.WriteLine($"Enter the Ages of Student in the track {i + 1}: ");

                for (int j = 0; j < Num_Std; j++)
                {
                    Console.WriteLine($"Enter the Ages of Student #{j + 1}: ");
                    Tracks[i][j] = int.Parse(Console.ReadLine());
                }
            }
               for (int t = 0; t < Num_Tracks; t++)
               {
                   int sum = 0;
                   Console.Write("Track " + (t + 1) + " ages: \n");

                   for (int s = 0; s < Tracks[t].Length; s++)
                   {   
                       Console.WriteLine(Tracks[t][s] + " ");
                       sum += Tracks[t][s];
                   }
                   double average = (double)sum / Tracks[t].Length;
                   Console.WriteLine("Average Age = " + average + "\n");
               }
            }
        }
    }

