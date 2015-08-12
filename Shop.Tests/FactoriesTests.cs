using FluentAssertions;
using Shop.Domain;
using Shop.Domain.Entities;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class FactoriesTests
    {
        [Theory]
        [ShopAutoData]
        public void cart_factory_should_create_cart_with_proper_product_inside(
            CartFactory sut)
        {
            var product = new ProductDataFactory()
                .CreateProduct();

            var actual = sut.CreateCart(product);
            
            actual
                .Should()
                .BeOfType<Cart>();
            actual
                .Products
                .Count
                .Should()
                .Be(1);
            actual
                .Products
                .Should()
                .Contain(product);
        }
    }
}
