using System.Web.Http;
using Shop.Domain.Entities;
using Shop.Domain.Services;
using Shop.Site.Models;

namespace Shop.Site.Controllers
{
    public class CartController : ApiController
    {
        private readonly ICartService cartService;

        public CartController(ICartService service)
        {
            cartService = service;
        }

        public Cart Post(AddToCartData data)
        {
            return cartService.AddProductToCart(data.CartId, data.ProductId);
        }
    }
}
