using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class ArticleServiceTests
    {
        [Theory] 
        [ShopControllerAutoData] 
        public void get_all_articles_returns_empty_list_if_no_articles_stored(
            [Frozen] IArticleRepository repo,
            ArticleService sut,
            List<Article> articles)
        {
            repo.GetAll()
                .Returns(new List<Article>());

            sut.GetAllArticles()
                .ShouldAllBeEquivalentTo(new List<Article>());
        }
        
        [Theory]
        [ShopControllerAutoData]
        public void get_all_articles_returns_list_of_all_stored_articles(
            [Frozen] IArticleRepository repo,
            ArticleService sut,
            List<Article> articles)
        {
            repo.GetAll()
                .Returns(articles);

            sut.GetAllArticles()
                .ShouldAllBeEquivalentTo(articles);
        }
    }
}
