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

        public Cart AddProductToCart(Guid? cartId, Product product)
        {
            Cart cart = null;

            if (cartId.HasValue)
            {
                cart = repository.GetCartById(cartId.Value);
                cart.AddProduct(product);
            }
            else
            {
                cart = factory.CreateCart(product);
            }

            return repository.Save(cart);
        }
    }
}
