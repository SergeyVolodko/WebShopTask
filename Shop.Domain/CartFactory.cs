using Shop.Domain.Entities;

namespace Shop.Domain
{
    public class CartFactory: ICartFactory
    {
        public Cart CreateCart()
        {
            return new Cart();
        }
    }
}
