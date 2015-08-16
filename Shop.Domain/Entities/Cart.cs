using System;
using System.Collections.Generic;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public virtual Guid? Id { get; set; }
        public virtual List<Product> Products { get; set; }

        public Cart()
        {
            this.Products = new List<Product>();
        }

        public virtual void AddProduct(Product product)
        {
            product.Carts.Add(this);
            this.Products.Add(product);
        }
        
    }
}