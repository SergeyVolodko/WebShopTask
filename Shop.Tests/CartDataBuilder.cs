using System;
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Tests
{
    public class CartDataBuilder
    {
        private readonly Cart cart;

        public CartDataBuilder()
        {
            cart = new Cart();
            cart.Items = new List<CartItem>();
        }

        public CartDataBuilder WithProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                cart.AddProduct(product);
            }
            return this;
        }
        
        public CartDataBuilder WithProduct(Product product)
        {
            cart.AddProduct(product);
            return this;
        }
        
        public CartDataBuilder WithProduct(Product product, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                cart.AddProduct(product);
            }
            
            return this;
        }

        public CartDataBuilder WithEmptyId()
        {
            cart.Id = null;
            return this;
        }
        
        public CartDataBuilder WithSomeId()
        {
            cart.Id = Guid.NewGuid();
            return this;
        }

        public CartDataBuilder WithId(Guid cartId)
        {
            cart.Id = cartId;
            return this;
        }

        public Cart Build()
        {
            return cart;
        }
    }
}
