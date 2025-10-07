using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_C_
{
    internal class Anonymous_Object_Creation
    {
        public  class Product
        {
            public string name { get; set; }   
            public int Price { get; set; }

           public Product() {
                name = string.Empty;
                Price = 0;
            }

            public override string ToString() {
                return $"the Name: {name}, and The Price: {Price}.  ";
            }

        }

        public static void task1()
        {
            dynamic createProduct(string n, int p)
            {
                Product product = new Product() { name = n, Price = p };

                dynamic anonProduct = new
                {
                    Product_Name = n,
                    Price = p
                };
                return anonProduct;
            }
            Console.WriteLine(createProduct("laptop", 50000));
        }
    }
}
