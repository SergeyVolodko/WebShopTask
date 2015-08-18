using System;
using System.Collections.Generic;

namespace Shop.Domain.Entities
{
    public class ShopOrder
    {
        public virtual Guid Id { get; set; }
        
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Zip { get; set; }

        public virtual User User { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }
}
