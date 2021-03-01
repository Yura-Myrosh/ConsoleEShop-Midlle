using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleEShop.Tests
{
    public class AdminTest
    {
        [Fact]
        public void Can_Create_Admin_Test_12345Test()
        {
            //Arrange
            var admin = new Admin("Test", "12345Test");
            //Act
            UsersReg.AddUser(admin);
            //Assert
            Assert.Contains(admin, UsersReg.Users);
        }

        [Fact]
        public void Can_Get_All_Users_Info()
        {
            //Arrange
            var user = UsersReg.Users[1] as Admin;
            var allInfo = string.Join("\n", UsersReg.Users);
            //Act
            var getInfo = user.GetUsers();
            //Assert
            Assert.Equal(allInfo, getInfo);
        }

        [Fact]
        public void Can_Add_New_Product_Apple()
        {
            //Arrange
            var user = UsersReg.Users[1] as Admin;
            var product = new Product() { Name = "Apple" };
            //Act
            user.AddNewProduct(product);
            //Assert
            Assert.Contains(product, Store.Products);
        }

        [Fact]
        public void Can_Change_Info_About_Product_From_Apple_To_Test()
        {
            //Arrange
            var user = UsersReg.Users[1] as Admin;
            var product = new Product() { ProductID = 10, Name = "Apple" };
            Store.Products.Add(product);
            var newName = "Test";
            //Act
            user.ChangeInfoAboutProduct(product.ProductID, name: newName);
            //Assert
            Assert.Equal(newName, product.Name);
        }

        [Fact]
        public void Can_Change_Order_From_New_To_Canceled()
        {
            //Arrange
            var user = UsersReg.Users[1] as Admin;
            var order = new Order() { OrderID = 1 };
            Store.Orders.Add(UsersReg.Users[0], new List<Order>() { order });
            //Act
            user.ChangeOrderStatus(order.OrderID, Status.Canceled);
            //Assert
            Assert.Equal(Status.Canceled, order.Status);
        }
    }
}
