using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Map
{
    public class CartMap: ClassMap<Cart>
    {
        public CartMap()
        {
            Table("Cart");

            Id(x => x.Id).GeneratedBy.GuidComb();

            HasManyToMany(x => x.Products);
        }
    }
}