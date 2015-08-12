using FluentNHibernate.Mapping;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.NHibernate.Map
{
    public class CartMap: ClassMap<CartDto>
    {
        public CartMap()
        {
            Table("Cart");

            Id(x => x.Id).GeneratedBy.GuidComb();

            HasManyToMany(x => x.Products)
               .Table("ProductInCart")
               .ParentKeyColumn("product_fk")
               .ChildKeyColumn("cart_fk")
               .Cascade.SaveUpdate();
        }
    }
}