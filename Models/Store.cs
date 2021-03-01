using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Models
{
    public static class Store
    {
        public static List<Product> Products { get; private set; } = new List<Product>();
        public static Dictionary<Registered, List<Order>> Orders { get; set; } = new Dictionary<Registered, List<Order>>();
        static Store()
        {
            Products.AddRange(
                new List<Product>
                {
                    new Product()
                    {
                        ProductID = 1,
                        Name = "First",
                        Description = "My first product",
                        Price = 1000.00m,
                        Category = "Numbers"
                    },
                    new Product()
                    {
                        ProductID = 2,
                        Name = "Second",
                        Description = "My second product",
                        Price = 15000.00m,
                        Category = "Numbers"
                    },
                    new Product()
                    {
                        ProductID = 3,
                        Name = "Bread",
                        Description = "Smth for eat",
                        Price = 1000.00m,
                        Category = "food"
                    }
                });
        }
        public static void Save()
        {
            foreach (var item in UsersReg.Users)
            {
                if (!Orders.ContainsKey(item))
                {
                    Orders.Add(item, item?.Orders);
                }
                else
                {
                    Orders[item].AddRange(item.Orders);
                }
                
            }
        }
    }
}
