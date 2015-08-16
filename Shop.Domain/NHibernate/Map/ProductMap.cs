using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Map
{
    public class ProductMap: ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Price);

            HasManyToMany(x => x.Carts);
        }
    }
}