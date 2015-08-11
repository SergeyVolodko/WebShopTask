using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Site.Controllers;
using Shop.Site.Models;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class CartControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void post_article_to_empty_cart_returns_cart_with_this_article(
            [Frozen] ICartService service,
            CartController sut)
        {
            var article = new ArticleDataFactory()
                .CreateArticle();
            var cart = new CartDataBuilder()
                .WithArticle(article)
                .Build();

            service.AddArticleToCart(null, article)
                .Returns(cart);

            var data = new AddToCartData
            {
                CartId = null,
                Article = article
            };

            sut.Post(data)
                .Articles
                .Should()
                .Contain(article);
        }
    }
}
