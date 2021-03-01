using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Exeptions
{
    public class LoginException : Exception
    {
        public LoginException() : base() { }
        public LoginException(string message) : base(message) { }
    }
}
