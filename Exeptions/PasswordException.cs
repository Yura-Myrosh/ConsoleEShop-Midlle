using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Exeptions
{
    public class PasswordException: Exception
    {
        PasswordException() : base() { }
        public PasswordException(string message) : base(message) { }
    }
}
