using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleEShop.Tests
{
    public class GuestTest
    {
        [Fact]
        public void Can_Create_Guest()
        {
            //Arrange
            var g = new Guest();
            //Assert
            Assert.NotNull(g);
        }

        [Fact]
        public void Can_Get_Products_From_Store_And_Methods()
        {
            //Arrange
            IUser g = new Guest();
            //Act
            var products = Store.Products;
            //Assert
            Assert.Equal(g.GetAllProducts(), products);
        }

        [Fact]
        public void Can_LogIn_Test_12345678TEST()
        {
            //Arrange
            Guest g = new Guest();
            UsersReg.AddUser(new RegisteredUser("Test", "12345678TEST"));
            //Act
            var newUser = g.LogIn("Test", "12345678TEST");
            //Assert
            Assert.Contains(newUser, UsersReg.Users);
        }

        [Fact]
        public void Can_Create_NewUser_Test_Test12345()
        {
            //Arrange
            Guest g = new Guest();
            //Act
            var newUser = g.CreateNewRegisteredUser("Test", "Test12345");
            //Assert
            Assert.Contains(newUser, UsersReg.Users);
        }

        [Fact]
        public void Can_Find_Product_Apple()
        {
            //Arrange
            IUser g = new Guest();
            var prod = new Product() { Name = "Apple" };
            Store.Products.Add(prod);
            //Act
            var find = g.FindProductByName("Apple");
            //Assert
            Assert.Contains(find, Store.Products);
        }
    }
}
