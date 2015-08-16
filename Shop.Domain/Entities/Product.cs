using System;
using System.Collections.Generic;

namespace Shop.Domain.Entities
{
    public class Product
    {
        public virtual Guid? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }

        public virtual IList<Cart> Carts { get; set; }
    }
}
