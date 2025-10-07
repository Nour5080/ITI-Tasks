using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_Day_1
{
    internal class Querys2
    {
        public static void task2()
        {
            string[] names = { "Tom", "Dick", "Harry", "MARY", "Jay" };

            var ThreeIndexName = names.Where(x => x.Length == 3).Select(x=>x);
            foreach (var name in ThreeIndexName)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("=================================================");

            var ContainA=names.Where(x=>x.ToLower().Contains('a')).OrderBy(x=>x.Length);
            
            foreach (var name in ContainA)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("=================================================");

            var Top2 = names.Take(2);

            foreach (var name in Top2)
            {
                Console.WriteLine(name);
            }

        }
    }
}
