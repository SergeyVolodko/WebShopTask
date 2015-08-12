using System;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class CartServiceTests
    {
        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_invoke_factory_create_cart(
            [Frozen] ICartFactory factory,
            CartService sut)
        {
            var product = new ProductDataFactory()
                .CreateProduct();

            sut.AddProductToCart(null, product);

            factory
                .Received()
                .CreateCart(product);
        }
        
        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_invoke_repo_save(
            [Frozen] ICartRepository repo,
            [Frozen] ICartFactory factory,
            CartService sut)
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

            repo.Received()
                .Save(cart);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_nonempty_cart_not_invoke_factory_create_cart(
            [Frozen] ICartFactory factory,
            [Frozen] ICartRepository repo,
            CartService sut,
            Guid cartId,
            Prdouct prdouct)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .WithProduct(prdouct)
                .Build();

            repo.GetCartById(cartId)
                .Returns(cart);

            sut.AddProductToCart(cartId, Arg.Any<Prdouct>());

            factory.DidNotReceive()
                .CreateCart(Arg.Any<Prdouct>());
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_nonempty_cart_invoke_repo_get_by_id(
            [Frozen] ICartRepository repo,
            CartService sut,
            Prdouct prdouct,
            Guid cartId)
        {
            sut.AddProductToCart(cartId, prdouct);

            repo.Received()
                .GetCartById(cartId);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_existung_cart_invoke_repo_save(
            [Frozen] ICartRepository repo,
            CartService sut,
            Prdouct prdouct,
            Guid cartId)
        {
            var cart = new CartDataBuilder()
                .WithId(cartId)
                .Build();

            repo.GetCartById(cartId)
                .Returns(cart);

            sut.AddProductToCart(cartId, prdouct);

            repo.Received()
                .Save(cart);
        }
    }
}
