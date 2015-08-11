using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Dto
{
    public class CartDto
    {
        public virtual Guid? Id { get; set; }
        public virtual IList<ArticleDto> Articles { get; set; }

        public CartDto()
        {
            this.Articles = new List<ArticleDto>();
        }

        public virtual void AddArticle(ArticleDto article)
        {
            article.Carts.Add(this);
            this.Articles.Add(article);
        }

        public static implicit operator Cart(CartDto dto)
        {
            var articles = dto.Articles.Select(article => (Article) article).ToList();

            var cart = new DtoMapper<Cart>()
                .MapFrom(dto);

            cart.Articles = articles;

            return cart;
        }
    }
}