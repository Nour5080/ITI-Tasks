using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_C_
{
    internal class FixedSizeList
    {
    
        public class FixedSize<T> { 
            
            public T[] items;
            public int count;

            public FixedSize(int Capacity) {
                if (Capacity < 0)
                throw new ArgumentException("Capacity must be > 0");
                items = new T[Capacity];
                count = 0;
            }

            public void Add(T item) {
                if (count >= items.Length)
                    throw new ArgumentException("The List is Already Full");

                items[count++] = item;
            }

            public T Get(int index) {
                if (index >= count || index < 0)
                    throw new ArgumentException("Invalid Index");
                return items[index];
            }
        
        }


        public static void task5()
        {
         var Flist=new FixedSize<string>(3);

            Flist.Add("Nour");
            Flist.Add("Mohamed");
            Flist.Add("Mahmoud");
            Console.WriteLine($"FixedSizeList [0]: {Flist.Get(0)}");
            Console.WriteLine($"FixedSizeList [1]: {Flist.Get(1)}");
            Console.WriteLine($"FixedSizeList [2]: {Flist.Get(2)}");


        }
    }
}
