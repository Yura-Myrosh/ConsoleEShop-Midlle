using ConsoleEShop.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Models
{
    public interface IUser
    {
        public virtual List<Product> GetAllProducts() => Store.Products;
       
        public virtual Product FindProductByName(string name)
        {
            var product = Store.Products.Find(x => x.Name == name);
            if (product == null)
            {
                throw new ProductException( "You enter wrong product's name");
            }
            return product;
        }
    }
}
