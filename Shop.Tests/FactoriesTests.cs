using FluentAssertions;
using Shop.Domain.Entities;
using Shop.Domain.Factories.Impl;
using Shop.Tests.DataCreation;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class FactoriesTests
    {
        [Theory]
        [ShopAutoData]
        public void cart_factory_should_create_empty_cart(
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
        
        [Theory]
        [ShopAutoData]
        public void order_factory_should_order_with_proper_user_and_order_items(
            OrderFactory sut,
            User user,
            string address,
            string city,
            string zip)
        {
            var products = new ProductDataFactory()
                .CreateSavedProductsList(5);

            var cart = new CartDataBuilder()
                .WithSomeId()
                .WithProducts(products)
                .Build();

            var actual = sut.CreateOrder(user, 
                                         cart,
                                         address, 
                                         city, 
                                         zip);

            actual
                .Should()
                .NotBeNull();
            
            actual
                .Should()
                .BeOfType<ShopOrder>();
            
            actual.User
                .ShouldBeEquivalentTo(user);
            actual.OrderItems
                .ShouldAllBeEquivalentTo(cart.Items);

            actual.Address
                .Should().Be(address);
            actual.City
                .Should().Be(city);
            actual.Zip
               .Should().Be(zip);
        }
    }
}
