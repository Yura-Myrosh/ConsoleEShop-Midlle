using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"Id - {ProductID}:  {Name} - {Price :C2}";
        }
    }
}
