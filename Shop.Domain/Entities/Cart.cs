using System;
using System.Collections.Generic;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public virtual Guid? Id { get; set; }
        public virtual IList<CartItem> Items { get; set; }

        public Cart()
        {
            this.Items = new List<CartItem>();
        }

        public virtual void AddProduct(Product product)
        {
            this.Items.Add(new CartItem(product));
        }
        
    }
}