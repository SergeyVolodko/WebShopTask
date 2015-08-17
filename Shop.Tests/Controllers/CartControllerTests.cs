using System;
using FluentAssertions;
using NSubstitute;
using Shop.Domain.Entities;
using Shop.Domain.Services;
using Shop.Site.Controllers;
using Shop.Site.Models;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class CartControllerTests
    {
        private readonly ICartService service;
        private readonly CartController sut;

        public CartControllerTests()
        {
            service = Substitute.For<ICartService>();
            sut = new CartController(service);
        }

        [Theory]
        [ShopAutoData]
        public void post_product_to_empty_cart_returns_cart_with_this_product(
            Product product)
        {
            var cart = new CartDataBuilder()
                .WithProduct(product)
                .Build();

            service.AddProductToCart(null, product.Id.Value)
                .Returns(cart);

            var data = new AddToCartData
            {
                CartId = null,
                ProductId = product.Id.Value
            };

            sut.Post(data)
                .Items
                .Should()
                .Contain(item => item.Product == product);
        }

        [Theory]
        [ShopAutoData]
        public void post_product_invokes_service_add_product_to_cart_method_with_proper_arguments(
            Guid? cartId,
            Guid productId)
        {
            var data = new AddToCartData
            {
                CartId = cartId,
                ProductId = productId
            };

            sut.Post(data);

            service.Received()
                .AddProductToCart(cartId, productId);
        }
    }
}
