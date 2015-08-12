using FluentNHibernate.Mapping;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.NHibernate.Map
{
    public class ProductMap: ClassMap<ProductDto>
    {
        public ProductMap()
        {
            Table("Prdouct");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Price);

            HasManyToMany(x => x.Carts)
                .Table("ProductInCart")
                .ParentKeyColumn("cart_fk")
                .ChildKeyColumn("product_fk")
                .Inverse()
                .Cascade.SaveUpdate();
        }
    }
}