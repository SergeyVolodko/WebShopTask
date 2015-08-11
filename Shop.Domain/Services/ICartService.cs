using System;
using Shop.Domain.Entities;

namespace Shop.Domain.Services
{
    public interface ICartService
    {
        Cart AddArticleToCart(Guid? cartId, Article article);
    }
}
