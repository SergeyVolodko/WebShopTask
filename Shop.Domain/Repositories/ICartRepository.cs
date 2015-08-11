using System;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface ICartRepository
    {
        Cart Save(Cart cart);
        Cart GetCartById(Guid cartId);
    }
}
