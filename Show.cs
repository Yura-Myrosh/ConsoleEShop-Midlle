using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop
{
    public static class Show
    {
        public static void GetAvailableActions(IUser curent_User)
        {
            switch (curent_User.GetType().Name)
            {
                case "Guest":
                    {
                        ShowGusetAction();
                        break;
                    }
                case "RegisteredUser":
                    {
                        ShowRegisteredUser();
                        break;
                    }
                case "Admin":
                    {
                        ShowAdminAction();
                        break;
                    }
                default: throw new Exception();
            }
        }

        private static void ShowAdminAction()
        {
            Console.WriteLine("\nFor choose action pres number 1-10:");
            Console.WriteLine("1 - Look all products");
            Console.WriteLine("2 - Search product by name");
            Console.WriteLine("3 - Add product to cart");
            Console.WriteLine("4 - Create order");
            Console.WriteLine("5 - Close order");
            Console.WriteLine("6 - Show users info");
            Console.WriteLine("7 - Change users info");
            Console.WriteLine("8 - Add new product");
            Console.WriteLine("9 - Change info about product");
            Console.WriteLine("10 - Change order status");
            Console.WriteLine("11 - Log out");
            Console.WriteLine("12 - Exit program");


        }

        private static void ShowGusetAction()
        {
            Console.WriteLine("\nFor choose action pres number 1-5:");
            Console.WriteLine("1 - Look all products");
            Console.WriteLine("2 - Search product by name");
            Console.WriteLine("3 - Create account");
            Console.WriteLine("4 - Log in account");
            Console.WriteLine("5 - Exit program");
        }
        private static void ShowRegisteredUser()
        {
            Console.WriteLine("\nFor choose action pres number 1-10:");
            Console.WriteLine("1 - Look all products");
            Console.WriteLine("2 - Search product by name");
            Console.WriteLine("3 - Add product to cart");
            Console.WriteLine("4 - Create order");
            Console.WriteLine("5 - Close order");
            Console.WriteLine("6 - Show history of orders");
            Console.WriteLine("7 - Indicate that the products have been received");
            Console.WriteLine("8 - Change name or password");
            Console.WriteLine("9 - Log out");
            Console.WriteLine("10 - Exit program");
        }
    }
}
