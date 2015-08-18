using System;

namespace Shop.Domain.Entities
{
    public class OrderItem
    {
        public virtual Guid? Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
