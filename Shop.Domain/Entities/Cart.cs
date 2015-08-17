using System;
using System.Collections.Generic;
using System.Linq;

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
            var existing = this.Items.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                this.Items.Add(new CartItem(product));
            }
        }
        
    }
}