using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IOrderRepository
    {
        ShopOrder Save(ShopOrder order);
    }
}
