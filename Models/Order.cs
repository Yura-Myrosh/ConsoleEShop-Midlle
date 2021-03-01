using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop.Models
{
    public enum Status
    {
        New,        
        Paid,
        Sent,
        Received,
        Completed,
        Canceled
    }
    public class Order
    {
        public int OrderID { get; set; } = 0;
        public ICollection<CartLine> Lines { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }       
        public string City { get; set; }
        public string Country { get; set; }
        public bool GiftWrat { get; set; }
        public DateTime Time { get; } = DateTime.Now;
        public Status Status { get; set; } = Status.New;

        public override string ToString()
        {
            return $"IrderId: {OrderID} " +
                $"Name: {Name}, " +
                $"street: {Street}, " +
                $"city: {City}, " +
                $"contry: {Country}, " +
                $"time: {Time.ToShortDateString()} " +
                $"and " + (GiftWrat ? "you want a gift" : "you don't want a gift") +
                $"Status: {Status}"+
                $"\nProducts: {String.Join("", Lines.Select(x => x.ToString()))}";
        }
    }
}
