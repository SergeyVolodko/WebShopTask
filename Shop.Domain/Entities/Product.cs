using System;

namespace Shop.Domain.Entities
{
    public class Product
    {
        public virtual Guid? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
    }
}
