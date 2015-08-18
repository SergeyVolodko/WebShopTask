using System.Linq;
using Shop.Domain.Entities;

namespace Shop.Domain.Factories.Impl
{
    public class OrderFactory: IOrderFactory
    {
        public ShopOrder CreateOrder(User user, Cart cart, string address, string city, string zip)
        {
            var orderItems = cart.Items.Select(cartItem => new OrderItem{
                                                    Product = cartItem.Product, 
                                                    Quantity = cartItem.Quantity
                                                }).ToList();

            return new ShopOrder
            {
                User = user,
                //OrderItems = orderItems,
                Address = address,
                City = city,
                Zip = zip
            };
        }
    }
}
