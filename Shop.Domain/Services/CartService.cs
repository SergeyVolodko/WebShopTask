using System;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartFactory factory;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, ICartFactory factory)
        {
            this.factory = factory;
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
        }
        
        public Cart AddProductToCart(Guid? cartId, Guid productId)
        {
            Cart cart = null;

            var product = productRepository.GetById(productId);

            if (cartId.HasValue)
            {
               cart = cartRepository.GetCartById(cartId.Value);
            }
            else
            {
                cart = factory.CreateCart();
            }

            cart.AddProduct(product);
            
            cartRepository.Save(cart);

            return cart;
        }
    }
}
