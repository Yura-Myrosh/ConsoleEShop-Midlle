using ConsoleEShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleEShop.Tests
{
    public class OrderTest
    {
        [Fact]
        public void Can_Create_Order()
        {
            //Arrange
            var order = new Order();
            //Act
            var isExist = order != null;
            //Assert
            Assert.True(isExist);
        }
        
        [Fact]
        public void Can_Get_Lines()
        {
            //Arrange
            var order = new Order() { Lines = new List<CartLine> { new CartLine { CartLineID = 1} } };
            //Act
            var isExist = order.Lines.Count != 0;
            //Assert
            Assert.True(isExist);
        }
        [Fact]
        public void Is_Status_New()
        {
            //Arrange
            var order = new Order();
            //Act
            var isNew = order.Status == Status.New;
            //Assert
            Assert.True(isNew);
        }
    }
}
