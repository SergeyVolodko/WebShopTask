using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Site.Controllers;
using Shop.Site.Models;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class CartControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void post_product_to_empty_cart_returns_cart_with_this_product(
            [Frozen] ICartService service,
            CartController sut)
        {
            var product = new ProductDataFactory()
                .CreateProduct();
            var cart = new CartDataBuilder()
                .WithProduct(product)
                .Build();

            service.AddProductToCart(null, product)
                .Returns(cart);

            var data = new AddToCartData
            {
                CartId = null,
                Prdouct = product
            };

            sut.Post(data)
                .Products
                .Should()
                .Contain(product);
        }
    }
}
