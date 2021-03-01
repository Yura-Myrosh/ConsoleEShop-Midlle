using ConsoleEShop.Exeptions;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop.Models
{
    public class RegisteredUser : Registered, IUser
    {
        public RegisteredUser(string login, string password)
        {            
            LoginName = login;
            Password = password;
            Cart = new Cart();            
            Orders = new List<Order>();
        }               

        public void SetStatusReceived(int orderId)
        {
            var order = Orders.Find(x => x.OrderID == orderId);
            if (order == null || order.Status == Status.Canceled)
            {
                throw new OrderException("Absent order");
            }
            if (order.Status == Status.Received)
            {
                throw new OrderException("This order was receved!");
            }
            order.Status = Status.Received;
            Orders.Remove(Orders.Find(x => x.OrderID == orderId));
            Store.Save();
        }        

        public string GetHistoryOfOrders() => !Orders.Any() ? "No histrory" : string.Join("", Orders.Select(x => x.ToString()));

        public void ChangeName(string name) => LoginName = name;

        public void ChangePassword(string password) => Password = password;
       
        
    }
}
