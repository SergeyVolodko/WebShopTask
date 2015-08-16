using System;
using NSubstitute;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class CartServiceTests
    {
        private readonly ICartFactory factory;
        private readonly ICartRepository repository;
        private readonly CartService sut;

        public CartServiceTests()
        {
            factory = Substitute.For<ICartFactory>();
            repository = Substitute.For<ICartRepository>();
            sut = new CartService(repository, factory);
        }

        [Fact]
        public void add_product_to_empty_cart_invoke_factory_create_cart()
        {
            var product = new ProductDataFactory()
                .CreateProduct();

            sut.AddProductToCart(null, product);

            factory
                .Received()
                .CreateCart(product);
        }
        
        [Fact]
        public void add_product_to_empty_cart_invoke_repo_save()
        {
            var product = new ProductDataFactory()
                .CreateProduct();

            var cart = new CartDataBuilder()
                .WithEmptyId()
                .WithProduct(product)
                .Build();

            factory.CreateCart(product)
                .Returns(cart);
            
            sut.AddProductToCart(null, product);

            repository.Received()
                      .Save(cart);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_nonempty_cart_not_invoke_factory_create_cart(
            Guid cartId,
            Product product)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .WithProduct(product)
                .Build();

            repository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, Arg.Any<Product>());

            factory.DidNotReceive()
                   .CreateCart(Arg.Any<Product>());
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

            repository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, product);

            repository.Received()
                      .GetCartById(cartId);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_existung_cart_invoke_repo_save(
            Product product,
            Guid cartId)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            repository.GetCartById(cartId)
                      .Returns(cart);

            sut.AddProductToCart(cartId, product);

            repository.Received()
                      .Save(cart);
        }
    }
}
