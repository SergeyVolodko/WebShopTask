using Shop.Domain.Entities;

namespace Shop.Domain.Factories
{
    public interface IOrderFactory
    {
        ShopOrder CreateOrder(User user, Cart cart, string address, string city, string zip);
    }
}
