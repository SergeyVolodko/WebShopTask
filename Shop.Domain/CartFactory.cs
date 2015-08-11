using Shop.Domain.Entities;

namespace Shop.Domain
{
    public class CartFactory: ICartFactory
    {
        public Cart CreateCart(Article article)
        {
            var cart = new Cart();
            cart.AddArticle(article);

            return cart;
        }
    }
}
