using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Exeptions
{
    class ExitException : Exception
    {
        public ExitException(string message = "GoodbBye!!!") : base(message) { }
    }
}
