using System;

namespace Shop.Domain.Entities
{
    public class CartItem
    {
        public virtual Guid? Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Product Product { get; set; }

        public CartItem()
        {
        }

        public CartItem(Product product)
        {
            this.Product = product;
            this.Quantity = 1;
        }
    }
}
