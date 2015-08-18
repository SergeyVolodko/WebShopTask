using Shop.Domain.Entities;

namespace Shop.Domain.Factories.Impl
{
    public class CartFactory: ICartFactory
    {
        public Cart CreateCart()
        {
            return new Cart();
        }
    }
}
