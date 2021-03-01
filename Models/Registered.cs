using ConsoleEShop.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleEShop.Models
{
    public abstract class Registered
    {
        private string name;
        private string password;
        public string LoginName
        {
            get => name;
            set
            {
                if (UsersReg.Users.Any(x => x.LoginName == value))
                {
                    throw new LoginException("Other user have this name");
                }
                name = value;
            }
        }
        public string Password
        {
            get => password; 
            set
            {
               if (!CheckPassword(value))
                {
                    throw new PasswordException("The password does not meet the requirements");
                }
                password = value;
            }
        }
        public Cart Cart { get; protected set; }
        public List<Order> Orders { get; protected set; }

        public IUser LogOut()
        {
            Store.Save();
            return new Guest();
        }

        public virtual void AddProductToCart(string productName, int quantity)
        {
            var product = Store.Products.FirstOrDefault(x => x.Name == productName);
            if (product == null)
            {
                throw new ProductException($"Product: {productName} is absent in store");
            }
            Cart.AddItem(product, quantity);
        }
        public virtual void CreateNewOrder(string street, string city, string country, bool giftWrat)
        {
            if (Cart.Lines.Count == 0)
            {
                throw new OrderException("Empty cart!");
            }
            Orders.Add(new Order
            {
                OrderID = Orders.Count + 1,
                Street = street,
                City = city,
                Country = country,
                GiftWrat = giftWrat,
                Name = LoginName,
                Lines = Cart.Lines.ToList()
            });
            Store.Save();
            Cart.Clear();
        }

        public virtual void CancelOrder(int orderId)
        {
            var order = Orders.FirstOrDefault(x => x.OrderID == orderId);
            if (order == null || order.Status == Status.Canceled)
            {
                throw new OrderException("This order is absent");
            }
            if (order.Status == Status.Received)
            {
                throw new OrderException("This order was received");
            }
            order.Status = Status.Canceled;
            Store.Save();
        }

        private bool CheckPassword(string password)
        {
            Regex[] regices = new Regex[] { new Regex(@"[0-9]+"), new Regex("[A-Z]+"), new Regex(@".{8,}") };
            return regices[0].IsMatch(password) && regices[1].IsMatch(password) && regices[2].IsMatch(password);
        }

        public override string ToString()
        {
            return $"Login: {LoginName}\t" +
                    $"password: {Password}\t" +
                    $"Orders: {string.Join("\n", Orders)}";
        }
    }
}
