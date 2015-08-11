using FluentAssertions;
using Shop.Domain;
using Shop.Domain.Entities;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class FactoriesTests
    {
        [Theory]
        [ShopAutoData]
        public void cart_factory_should_create_cart_with_proper_article_inside(
            CartFactory sut)
        {
            var article = new ArticleDataFactory()
                .CreateArticle();

            var actual = sut.CreateCart(article);
            
            actual
                .Should()
                .BeOfType<Cart>();
            actual
                .Articles
                .Count
                .Should()
                .Be(1);
            actual
                .Articles
                .Should()
                .Contain(article);
        }
    }
}
