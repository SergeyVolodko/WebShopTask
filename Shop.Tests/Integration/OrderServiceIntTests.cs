using FluentAssertions;
using Ninject;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Repositories;
using Shop.Domain.Repositories.Impl;
using Shop.Domain.Services.Impl;
using Shop.Domain.Utils;
using Shop.Site;
using Shop.Tests.DataCreation;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class OrderServiceIntTests
    {

        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;
        private readonly IUserRepository userRepository;
        private readonly OrderService sut;

        public OrderServiceIntTests()
        {
            var kernel = new Global(Consts.TEST_APP_DATA).GetKernel();

            productRepository = kernel.Get<IProductRepository>();
            cartRepository = kernel.Get<ICartRepository>();
            userRepository = kernel.Get<IUserRepository>();

            sut = kernel.Get<OrderService>();
        }

        [Theory]
        [ShopAutoData]
        public void sucessful_scenario_of_order_creation(
            OrderData data)
        {
            // Arrange
            var products = new ProductDataFactory()
                .CreateSavedProductsList(3);
            productRepository.Save(products);

            var cart = new CartDataBuilder()
                .WithProducts(products)
                .Build();
            cartRepository.Save(cart);
            
            var user = new UserDataBuilder()
                .WithId(data.UserId)
                .Build();
            userRepository.CreateUser(user);

            data.CartId = cart.Id.Value;
            data.UserId = user.Id;

            //Act
            var actual = sut.CreateOrder(data);

            //Assert
            actual.User
                .ShouldBeEquivalentTo(user);
            //actual.OrderItems
            //    .ShouldAllBeEquivalentTo(cart.Items);

            actual.Address
                .Should().Be(data.Address);
            actual.City
                .Should().Be(data.City);
            actual.Zip
               .Should().Be(data.Zip);
        }
    }
}
