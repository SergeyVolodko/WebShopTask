using System;
using FluentAssertions;
using NSubstitute;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class CartServiceTests
    {
        private readonly ICartFactory factory;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly CartService sut;

        public CartServiceTests()
        {
            factory = Substitute.For<ICartFactory>();
            cartRepository = Substitute.For<ICartRepository>();
            productRepository = Substitute.For<IProductRepository>();
            sut = new CartService(cartRepository, productRepository, factory);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_cart_invoke_get_product_by_id(
            Guid? cartId,
            Guid productId)
        {
            sut.AddProductToCart(cartId, productId);

            productRepository.Received().
                GetById(productId);
        }
        
        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_invoke_factory_create_cart(
            Guid productId)
        {
            sut.AddProductToCart(null, productId);

            factory
                .Received()
                .CreateCart();
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_invoke_repo_save(
            Guid productId)
        {
            var cart = new CartDataBuilder()
                .Build();
            
            factory.CreateCart()
                .Returns(cart);

            sut.AddProductToCart(null, productId);

            cartRepository.Received()
                      .Save(cart);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_not_empty_cart_not_invoke_factory_create_cart(
            Guid cartId,
            Product product)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .WithProduct(product)
                .Build();

            cartRepository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, product.Id.Value);

            factory.DidNotReceive()
                   .CreateCart();
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_nonempty_cart_invoke_repo_get_by_id(
            Product product,
            Guid cartId)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            cartRepository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, product.Id.Value);

            cartRepository.Received()
                      .GetCartById(cartId);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_existung_cart_invoke_repo_save(
            Guid productId,
            Guid cartId)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            cartRepository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, productId);

            cartRepository.Received()
                      .Save(cart);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_cart_returns_cart_containing_proprt_cart_item(
            Guid cartId,
            Product product)
        {
            var productId = product.Id.Value;
            productRepository.GetById(productId)
                .Returns(product);

            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            cartRepository.GetCartById(cartId)
                      .Returns(cart);

            var actual = sut.AddProductToCart(cartId, productId);

            actual.Items
                .Should()
                .Contain(item => item.Product == product);
        }
    }
}
