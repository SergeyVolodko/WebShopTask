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
        public void cart_factory_should_create_cart(
            CartFactory sut)
        {

            var actual = sut.CreateCart();

            actual
                .Should()
                .NotBeNull();
            
            actual
                .Should()
                .BeOfType<Cart>();
        }
    }
}
