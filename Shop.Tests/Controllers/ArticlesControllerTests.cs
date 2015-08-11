using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Site.Controllers;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class ArticlesControllerTests
    {
        [Fact]
        public void fact()
        {
            1.Should().Be(1);
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_returns_list_of_all_stored_articles(
            [Frozen] IArticleService service,
            ArticleController sut)
        {
            var articles = new ArticleDataFactory()
                .CreateManyArticles();
            
            service.GetAllArticles()
                .Returns(articles);

            sut.Get()
                .ShouldAllBeEquivalentTo(articles);
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_invokes_article_service_get_all_articles(
            [Frozen] IArticleService service,
            ArticleController sut)
        {
            sut.Get();

            service.Received()
                .GetAllArticles();
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_page_articles_returns_list_of_10_articles(
            [Frozen] IArticleService service,
            ArticleController sut)
        {
            var articles = new ArticleDataFactory()
                .CreateArticlesList(10);

            service.GetTenArticlesFromIndex(0)
                .Returns(articles);

            sut.GetPageArticles(1)
                .ShouldAllBeEquivalentTo(articles);
        }
        
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 10)]
        [InlineData(3, 20)]
        public void get_page_articles_for_specific_page_should_call_service_with_proper_parameter(
            int pageNumber,
            int startIndex)
        {
            var service = Substitute.For<IArticleService>();
            var sut = new ArticleController(service);
            
            sut.GetPageArticles(pageNumber);

            service.Received()
                .GetTenArticlesFromIndex(startIndex);
        }
    }
}
