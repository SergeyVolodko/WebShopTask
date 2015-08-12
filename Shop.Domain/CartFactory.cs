using Shop.Domain.Entities;

namespace Shop.Domain
{
    public class CartFactory: ICartFactory
    {
        public Cart CreateCart(Prdouct prdouct)
        {
            var cart = new Cart();
            cart.AddProduct(prdouct);

            return cart;
        }
    }
}
