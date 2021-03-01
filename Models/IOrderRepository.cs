using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Models
{
    public interface IOrderRepository
    {
        List<Order> Orders { get; }
        void SaveOrdes(Order order);
    }
}
