using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEShop.Models
{
    public class Guest : IUser
    {
        public Registered CreateNewRegisteredUser(string login, string password)
        {
            var user = new RegisteredUser(login, password);
            UsersReg.AddUser(user);            
            return user;
        }
        public Registered LogIn(string login, string password) => UsersReg.GetUserByLoginAndPassword(login, password);      
        
    }
}
