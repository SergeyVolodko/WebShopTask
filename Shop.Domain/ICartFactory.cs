using Shop.Domain.Entities;

namespace Shop.Domain
{
    public interface ICartFactory
    {
        Cart CreateCart(Prdouct prdouct);
    }
}
