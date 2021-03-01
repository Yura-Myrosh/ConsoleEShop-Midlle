using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private Registered _user;
        public OrderRepository(Registered user)
        {
            if (UsersReg.Users.Any(u => u.LoginName == user.LoginName))
            {
                _user = user;
            }
            else
            {
                UsersReg.AddUser(user);
                _user = user;
            }          
            
        }
        
        public List<Order> Orders => Store.Orders[_user];
        
        public void SaveOrdes(Order order)
        {
            Store.Orders[_user].Add(order);
        }
    }
}
