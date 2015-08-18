using Shop.Domain.Entities;
using Shop.Domain.Factories;
using Shop.Domain.Repositories;
using Shop.Domain.Utils;

namespace Shop.Domain.Services.Impl
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IOrderFactory factory;

        public OrderService(IOrderRepository orderRepository,
                            IUserRepository userRepository,
                            ICartRepository cartRepository,
                            IOrderFactory factory)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.factory = factory;
        }

        public ShopOrder CreateOrder(OrderData data)
        {
            var user = userRepository.GetUserById(data.UserId);
            var cart = cartRepository.GetCartById(data.CartId);

            var order = factory.CreateOrder(user,
                                            cart,
                                            data.Address,
                                            data.City,
                                            data.Zip);
            orderRepository.Save(order);

            return order;
        }
    }
}
