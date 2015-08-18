using Shop.Domain.Entities;
using Shop.Domain.Utils;

namespace Shop.Domain.Services
{
    public interface IOrderService
    {
        ShopOrder CreateOrder(OrderData data);
    }
}
