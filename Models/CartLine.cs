using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"\n{Product} : {Quantity}";
        }
    }
}
