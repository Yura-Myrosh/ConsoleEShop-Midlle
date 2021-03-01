using ConsoleEShop.Exeptions;
using ConsoleEShop.Models;
using System;

namespace ConsoleEShop
{
    class Program
    {
        static IUser StartStore()
        {
            return new Guest();
        }
        static void Main(string[] args)
        {
            var curent_User = StartStore();           
           
            while (true)
            {
                Console.Clear();
                string state = curent_User.GetType().Name.ToLower() == "guest" ? "Guest" : (curent_User as Registered).LoginName;
                Console.WriteLine($"\nHello {state}");
                try
                {
                    Show.GetAvailableActions(curent_User);                   
                    var read = Console.ReadLine();
                    curent_User = Actions.DoAction(curent_User, read);
                    Console.WriteLine("Enter any button for continue...");
                    Console.ReadLine();
                }                
                catch (ExitException ex)
                {
                    Console.WriteLine(ex.Message);                    
                    return;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }   

            }
        }

       
    }
}
