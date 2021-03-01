using ConsoleEShop.Exeptions;
using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop
{
    public class Actions
    {
         static IUser _user;
        public static IUser DoAction(IUser user, string option)
        {
            _user = user;
            if (user is Guest)
            {
                DoGuestAction(option);               
            }
            else if (user is RegisteredUser)
            {
                DoRegisteredUserAction(option);
            }
            else if (user is Admin)
            {
                DoAdminAction(option);
              
            }
            return _user;
        }        
        

        private static void DoGuestAction(string option)
        {
            var u = _user as Guest;
            switch (option)
            {
                case "1":
                    {
                        foreach (var item in _user.GetAllProducts())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine(FindProduct(_user.FindProductByName));
                        break;
                    }
                case "3":
                    {
                        RegisterOrLogIn(u.CreateNewRegisteredUser);
                        break;
                    }
                case "4":
                    {
                        RegisterOrLogIn(u.LogIn);
                        break;
                    }
                case "5":
                    {
                        Exit();
                        break;
                    }
                default: WrongButtonPress();
                    break;
            }
        }

        private static void WrongButtonPress()
        {
            Console.WriteLine("You enter wrong command!!!!");
        }

        private static void DoRegisteredUserAction(string option)
        {
            var u = _user as RegisteredUser;
            switch (option)
            {
                case "1":
                case "2":
                    {
                        DoGuestAction(option);
                        break;
                    }
                case "3":
                    {
                        CreateOrder(u.AddProductToCart);
                        break;
                    }
                case "4":
                    {
                        TakeOrder(u.CreateNewOrder);
                        break;
                    }
                case "5":
                    {
                        CloseOrder(u.CancelOrder);
                        break;
                    }
                case "6":
                    {
                        ShowHistoryOfOrders(u.GetHistoryOfOrders);
                        break;
                       
                    }
                case "7":
                    {
                        SetStatusReceived(u.SetStatusReceived);
                        break;
                    }
                case "8":
                    {
                        Console.WriteLine("Enter new name or -: ");
                        var answer = Console.ReadLine();
                        if (answer !=  "-")
                        {
                            ChangeNameOrPassword(u.ChangeName, answer);
                        }
                        Console.WriteLine("Enter new password or -: ");
                        answer = Console.ReadLine();
                        if (answer != "-")
                        {
                            ChangeNameOrPassword(u.ChangePassword, answer);
                        }
                        break;
                    }
                case "9":
                    _user = u.LogOut();
                    break;
                case "10":
                    {
                        Exit();
                        break;
                    }
                default: WrongButtonPress();
                    break;
            }
        }

        private static void DoAdminAction(string option)
        {
            var u = _user as Admin;
            switch (option)
            {
                case "1":
                case "2":
                    DoRegisteredUserAction(option); break;
                case "3":
                    CreateOrder(u.AddProductToCart); break;
                case "4":
                    TakeOrder(u.CreateNewOrder); break;
                case "5":
                    CloseOrder(u.CancelOrder); break;
                case "6":
                    {
                        Console.WriteLine(u.GetUsers());
                        break;
                    }
                case "7":
                    {
                        ChangeUserInfo(u.ChangeUserLogin);
                        break;
                    }
                case "8":
                    {
                        AddProduct(u.AddNewProduct);
                        break;
                    }
                case "9":
                    {
                        ChangeProduct(u.ChangeInfoAboutProduct);
                        break;
                    }
                case "10":
                    {
                        ChangeOrderStatus(u.ChangeOrderStatus);
                        break;
                    }
                case "11": u.LogOut(); break;
                case "12": Exit(); break;
                default: WrongButtonPress(); break;
            }
        }

        private static void ChangeOrderStatus(Action<int, Status> changeOrderStatus)
        {
            Console.WriteLine("Enter orderId");
            var id = int.Parse(Console.ReadLine());
            for (int i = 0; i < (int)Status.Canceled; i++)
            {
                Console.WriteLine($"{i}:" + (Status)i);
            }
            Console.WriteLine("Enter status number: ");
            var stat = int.Parse(Console.ReadLine());
            if (stat > (int)Status.Canceled || stat < 0)
            {
                throw new OrderException("Wrong status!");
            }
        }

        private static void ChangeProduct(Action<int, string, string, decimal, string> changeInfoAboutProduct)
        {
            try
            {
                Console.WriteLine($"Choose id of product between 1 and {Store.Products.Count}");
                var product = Store.Products[int.Parse(Console.ReadLine())-1];                             
                var props = product.GetType().GetProperties();
                var param = new List<string>();
                var answ = string.Empty;
                foreach (var item in props)
                {
                    if (item.Name.ToLower() != "productid")
                    {
                        Console.WriteLine($"For {item.Name.ToLower()} curent value is {item.GetValue(product)}");
                        Console.Write($"Enter new {item.Name.ToLower()} or -:\t");
                        answ = Console.ReadLine();
                        param.Add(answ == "-" ? "" : answ);
                    }
                }
                changeInfoAboutProduct(Store.Products.IndexOf(product) + 1, param[0], param[1], param[2] == "" ? -1 : decimal.Parse(param[2].Replace('.', ',')), param[3]);

            }
            catch (OverflowException)
            {
                Console.WriteLine("You enter no-number value");
            }
            catch (FormatException)
            {
                Console.WriteLine("You enter no-number value");
            }
            catch (IndexOutOfRangeException) 
            {
                Console.WriteLine("You enter bad index");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You enter no-number value");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static void AddProduct(Action<Product> addNewProduct)
        {
            var product = new Product();
            try
            {
                product.ProductID = Store.Products.Count + 1;
                var props = product.GetType().GetProperties();
                foreach (var item in props.Select(x => new { Name = x.Name, Prop = x }))
                {
                    if (item.Name != "ProductID")
                    {
                        Console.WriteLine($"Enter {item.Name.ToLower()}");
                        var answer = Console.ReadLine();
                        if (item.Name != "Price")
                        {
                            item.Prop.SetValue(product, answer);
                        }
                        else
                        {
                            item.Prop.SetValue(product, decimal.Parse(answer.Replace('.', ',')));
                        }

                    }
                }
            }          
            catch (ArgumentException)
            {
                Console.WriteLine("You enter bad parametr");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            addNewProduct(product);
        }

        private static void ChangeUserInfo(Action<string, string> changeUserLogin)
        {
            try
            {
                Console.WriteLine("Enter old and new login splited by /");
                var logins = Console.ReadLine().Split("/");
                changeUserLogin(logins[0], logins[1]);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You forgot the login or split symbol(/)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Exit()
        {
            throw new ExitException();
        }

        private static void ChangeNameOrPassword(Action<string> action, string value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SetStatusReceived(Action<int> setStatusReceived)
        {
            try
            {
                Console.WriteLine("Enter the order id you received");
                setStatusReceived(int.Parse(Console.ReadLine()));
            }
            catch (IndexOutOfRangeException)
            {

                Console.WriteLine("You have entered a non-existent order number");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You did not enter anything or a letters");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }

        private static void CloseOrder(Action<int> cancelOrder)
        {
            try
            {
                Console.WriteLine("Enter the id of the order you want to cancel");
                cancelOrder.Invoke(int.Parse(Console.ReadLine()));
            }
            catch (IndexOutOfRangeException)
            {

                Console.WriteLine("You have entered a non-existent order number");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You did not enter anything or a letters");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowHistoryOfOrders(Func<string> getHistoryOfOrders)
        {
            try
            {
                Console.WriteLine(getHistoryOfOrders());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }

        private static void TakeOrder(Action<string, string, string, bool> createNewOrder)
        {
            try
            {
                var orderInfo = new List<string>();
                foreach (var item in createNewOrder.Method.GetParameters().Select(x => x.Name))
                {
                    if (item!= "GiftWrat")
                    {
                        Console.WriteLine($"Entrer {item}: ");
                    }
                    else
                    {
                        Console.WriteLine("Do you want gift? (Y/N)");
                    }
                    orderInfo.Add(Console.ReadLine());
                }
                createNewOrder.Invoke(orderInfo[0], orderInfo[1], orderInfo[2], orderInfo[3].ToLower() == "y");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);                
            }
        }

        private static void CreateOrder(Action<string, int> addProductToCart)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter product name and quantity splited by space: ");
                    var prod = Console.ReadLine().Split(" ");
                    addProductToCart.Invoke(prod[0], int.Parse(prod[1]));
                    Console.WriteLine("Product was added");                    
                    break;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Please enter number between 1 and {int.MaxValue}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                
            }
        }        

        private static void RegisterOrLogIn(Func<string, string, Registered> createNewRegisteredUser)
        {
            int i = 0;
            for (i = 0; i < 3; i++)
            {

                try
                {
                    Console.WriteLine("Enter login: ");
                    var login = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    var password = Console.ReadLine();
                    _user = (IUser)createNewRegisteredUser.Invoke(login, password);
                    i = 100;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (i >= 100)
            {
                Console.WriteLine("Successfully");
            }
            else
            {
                Console.WriteLine("Please, try later...");
            }
        }

        private static Product FindProduct(Func<string, Product> findProductByName)
        {
            Product product = null;
            try
            {
                Console.WriteLine("Enter product name: ");
                product = findProductByName.Invoke(Console.ReadLine());
            }
            catch (ProductException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return product;
           
        }
    }
}
