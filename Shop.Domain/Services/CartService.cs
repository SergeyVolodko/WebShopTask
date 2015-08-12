using System;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartFactory factory;
        private readonly ICartRepository repository;

        public CartService(ICartRepository repository, ICartFactory factory)
        {
            this.factory = factory;
            this.repository = repository;
        }

        public Cart AddProductToCart(Guid? cartId, Prdouct prdouct)
        {
            Cart cart = null;

            if (cartId.HasValue)
            {
                cart = repository.GetCartById(cartId.Value);
                cart.AddProduct(prdouct);
            }
            else
            {
                cart = factory.CreateCart(prdouct);
            }

            return repository.Save(cart);
        }
    }
}
