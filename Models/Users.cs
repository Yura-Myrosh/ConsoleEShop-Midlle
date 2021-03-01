using ConsoleEShop.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEShop.Models
{
    public static class UsersReg
    {

        public static List<Registered> Users { get; private set; } = new List<Registered>();
        static UsersReg()
        {
            
          Users.AddRange(
                new List<Registered>
                {
                    new RegisteredUser("Tom", "123456789Qwerty"),
                    new Admin("Admin", "123456789Qwerty")
                }
                );
        }
        public static void AddUser(Registered user)
        {
            if (Users.Any(x => x.LoginName == user.LoginName))
            {
                throw new LoginException("The user with this login is already registered");
            }
            else
            {
                Users.Add(user);
            }
        }
        public static Registered GetUserByLoginAndPassword(string login, string password)
        {
            var user = Users.FirstOrDefault(user => user.LoginName == login && user.Password == password);
            if (user != null)
            {
                return user;
            }
            throw new LoginException("Invalid login or password entered");
        }        
    }
}
