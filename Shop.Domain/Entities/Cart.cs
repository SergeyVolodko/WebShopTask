using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public Guid? Id { get; set; }
        public List<Article> Articles { get; set; }

        public Cart()
        {
            this.Articles = new List<Article>();
        }

        public virtual void AddArticle(Article article)
        {
            this.Articles.Add(article);
        }

        public static implicit operator CartDto(Cart cart)
        {
            var articleDTOs = cart.Articles
                .Select(article => (ArticleDto) article)
                .ToList();

            var cartDto = new DtoMapper<CartDto>()
                .MapFrom(cart);

            cartDto.Articles = articleDTOs;
            //foreach (var articleDto in articleDTOs)
            //{
            //    articleDto.Carts.Add(cartDto);
            //}

            return cartDto;
        }
    }
}