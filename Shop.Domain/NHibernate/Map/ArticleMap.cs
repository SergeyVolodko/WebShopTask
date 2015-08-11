using FluentNHibernate.Mapping;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.NHibernate.Map
{
    public class ArticleMap: ClassMap<ArticleDto>
    {
        public ArticleMap()
        {
            Table("Article");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Price);

            HasManyToMany(x => x.Carts)
                .Table("ArticleInCart")
                .ParentKeyColumn("cart_fk")
                .ChildKeyColumn("article_fk")
                .Inverse()
                .Cascade.SaveUpdate();
        }
    }
}