using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleEShop.Tests
{
    public class RegisteredUserTest
    {
        [Fact]
        public void Can_Create_Registered_User_Bill_12345Test()
        {
            //Arrange
            var user = new RegisteredUser("Bill", "12345Test");
            //Act
            UsersReg.AddUser(user);
            //Assert
            Assert.Contains(user, UsersReg.Users);
        }

        [Fact]
        public void Can_Add_Product_One_Apple_To_Cart_Tom_User()
        {
            //Arrange
            Registered user = UsersReg.Users[0];
            Product prod = new Product() { Name = "Apple" };
            Store.Products.Add(prod);
            //Act
            user.AddProductToCart(prod.Name, 1);
            //Assert
            Assert.Equal(user.Cart.Lines[0].Product.Name, prod.Name);
        }

        [Fact]
        public void Can_Take_Order_All_Field_Is_Test()
        {
            //Arrange
            Registered user = UsersReg.Users[0];
            user.AddProductToCart(Store.Products[0].Name, 1);
            //Act
            user.CreateNewOrder("Test", "Test", "Test", true);
            //Assert
            Assert.Single(user.Orders);
        }

        [Fact]
        public void Can_Cancel_Order_With_Id_1_From_User()
        {
            //Arrange
            var user = UsersReg.Users[0];
            var order = new Order() {Name = user.LoginName, OrderID = 1};
            user.Orders.Add(order);
            //Act
            user.CancelOrder(1);
            //Arrage
            Assert.Equal(Status.Canceled, user.Orders[0].Status);
        }

        [Fact]
        public void Can_Get_Orders_Historty_And_Status_Without_Orders()
        {
            //Arrange
            var user = UsersReg.Users[0] as RegisteredUser;
            //Act
            var history = user.GetHistoryOfOrders();
            //Assert
            Assert.Equal("No histrory", history);

        }

        [Fact]
        public void Can_Get_Orders_Historty_And_Status_With_Order()
        {
            //Arrange
            var user = UsersReg.Users[0] as RegisteredUser;
            var order = new Order() { Lines = new List<CartLine>() };
            user.Orders.Add(order);
            //Act
            var history = user.GetHistoryOfOrders();
            //Assert
            Assert.Equal(order.ToString(), history);

        }

        [Fact]
        public void Can_Set_Recieved_Status_From_OrderID_1()
        {
            //Arrange
            var user = UsersReg.Users[0] as RegisteredUser;
            var order = new Order() { OrderID = 1};
            user.Orders.Add(order);
            //Act
            user.SetStatusReceived(1);
            //Assert
            Assert.Equal(Status.Received, order.Status);
        }

        [Fact]
        public void Change_UserName_From_Tom_To_Test()
        {
            //Arrange
            var user = UsersReg.Users[0] as RegisteredUser;
            var newName = "Test";
            //Act
            user.ChangeName(newName);
            //Assert
            Assert.Equal(newName, user.LoginName);
        }

        [Fact]
        public void Chanhe_Users_Password_To_123456Test()
        {
            //Arrange
            var user = UsersReg.Users[0] as RegisteredUser;
            var newPassword = "123456Test";
            //Act
            user.ChangePassword(newPassword);
            //Assert
            Assert.Equal(newPassword, user.Password);
        }

        [Fact]
        public void Can_LogOut()
        {
            //Arrange
            var user = UsersReg.Users[0];
            //Act
            var newUser = user.LogOut() as Guest;
            //Assert
            Assert.NotNull(newUser);
        }
    }
}
