using Shop.Domain.Entities;

namespace Shop.Domain
{
    public class CartFactory: ICartFactory
    {
        public Cart CreateCart(Product product)
        {
            var cart = new Cart();
            cart.AddProduct(product);

            return cart;
        }
    }
}
