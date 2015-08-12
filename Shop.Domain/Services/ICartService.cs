using System;
using Shop.Domain.Entities;

namespace Shop.Domain.Services
{
    public interface ICartService
    {
        Cart AddProductToCart(Guid? cartId, Prdouct prdouct);
    }
}
