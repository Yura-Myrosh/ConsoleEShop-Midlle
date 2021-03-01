using ConsoleEShop.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop.Models
{
    public class Admin : Registered, IUser
    {
        public Admin(string login, string password)
        {
            LoginName = login;
            Password = password;
            Cart = new Cart();
            Orders = new List<Order>();
        }

        public Registered FindUser(string login)
        {
            var user = UsersReg.Users.FirstOrDefault(x => x.LoginName == login);
            if (user == null)
            {
                throw new LoginException("Failed to find this user");
            }
            return user;
        }

        public void ChangeUserLogin(string oldLogin, string newLogin)
        {
            try
            {
                var user = FindUser(oldLogin);
                user.LoginName = newLogin;
            }
            catch (LoginException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void AddNewProduct(Product product)
        {
            if (Store.Products.Any(p => p.Name == product.Name))
            {
                throw new ProductException("This product already exists");
            }            
            Store.Products.Add(product);
        }

        public void ChangeInfoAboutProduct(int productId, string name = "", string description = "", decimal price = -1, string category = "")
        {
            var product = Store.Products.FirstOrDefault(x => x.ProductID == productId);
            if (product == null)
            {
                throw new ProductException("Failed to find this product");
            }
            else
            {
                product.Name = name == "" ? product.Name : name;
                product.Description = description == "" ? product.Description : description;
                product.Category = category == "" ? product.Category : category;
                product.Price = price != -1? price : product.Price;
            }
        }
        public void ChangeOrderStatus(int orderId, Status status)
        {
            var allOrders = Store.Orders.Select(x => x.Value);
            Order order = null;
            foreach (var ordes in allOrders)
            {
                order = ordes.FirstOrDefault(x => x.OrderID == orderId);
                if (order != null)
                {
                    break;
                }
            }
            if (order == null)
            {
                throw new OrderException("Wrong orderId!!!");
            }
            if (order.Status != Status.Received)
            {
                order.Status = status;
            }
            else
            {
                throw new OrderException("This order was recived!!");
            }
        }
        public string GetUsers() => string.Join("\n", UsersReg.Users);
    }
}
