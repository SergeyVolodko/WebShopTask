using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
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
            ArticleController sut,
            List<Article> articles)
        {
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
    }
}
