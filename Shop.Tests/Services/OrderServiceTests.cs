using NSubstitute;
using Shop.Domain.Entities;
using Shop.Domain.Factories;
using Shop.Domain.Repositories;
using Shop.Domain.Services.Impl;
using Shop.Domain.Utils;
using Shop.Tests.DataCreation;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IOrderFactory factory;
        private readonly OrderService sut;

        public OrderServiceTests()
        {
            orderRepository = Substitute.For<IOrderRepository>();
            userRepository = Substitute.For<IUserRepository>();
            cartRepository = Substitute.For<ICartRepository>();
            factory = Substitute.For<IOrderFactory>();

            sut = new OrderService(orderRepository, userRepository, cartRepository, factory);
        }

        [Theory]
        [ShopAutoData]
        public void create_order_invokes_get_user_by_id(
            OrderData data)
        {
            sut.CreateOrder(data);

            userRepository.Received()
                .GetUserById(data.UserId);
        }

        [Theory]
        [ShopAutoData]
        public void create_order_invokes_get_cart_by_id(
            OrderData data)
        {
            sut.CreateOrder(data);

            cartRepository.Received()
                .GetCartById(data.CartId);
        }
        
        [Theory]
        [ShopAutoData]
        public void create_order_invokes_factory_create_order(
            OrderData data)
        {
            var cart = new CartDataBuilder()
                .WithId(data.CartId)
                .Build();
            
            var user = new UserDataBuilder()
                .WithId(data.UserId)
                .Build();

            cartRepository.GetCartById(data.CartId)
                .Returns(cart);

            userRepository.GetUserById(data.UserId)
                .Returns(user);

            sut.CreateOrder(data);

            factory.Received()
                .CreateOrder(user,
                             cart,
                             data.Address,
                             data.City,
                             data.Zip);
        }

        [Theory]
        [ShopAutoData]
        public void create_order_invokes_repository_save_order(
            OrderData data)
        {
            sut.CreateOrder(data);

            orderRepository.Received()
                .Save(Arg.Any<ShopOrder>());
        }
    }
}
