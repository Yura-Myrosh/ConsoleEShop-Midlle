using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Exeptions
{
    public class ProductException : Exception
    {
        public ProductException() : base() { }

        public ProductException(string message) : base(message) { }
    }
}
