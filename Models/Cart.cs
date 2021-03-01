using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEShop.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {           
            var line = Lines.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public decimal ComputeTotalValue() => Lines.Sum(e => e.Product.Price * e.Quantity);
        
        public virtual void Clear() => Lines.Clear();

        public override string ToString()
        {
            return "Current cart: " +
                String.Join("", Lines.Select(x => x.ToString())) +
                $"\nTotal: {ComputeTotalValue()}";
        }
    }
}
