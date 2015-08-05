using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
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
        [ShopAutoData] 
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
        [ShopAutoData]
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

        [Theory]
        [ShopAutoData]
        public void get_ten_articles_returns_proper_list_of_10_stored_articles(
            [Frozen] IArticleRepository repo,
            ArticleService sut)
        {
            var fixture = new Fixture();
            fixture.RepeatCount = 10;
            var articlesFrom0To10 = fixture.CreateMany<Article>().ToList();
            var articlesFrom10To20 = fixture.CreateMany<Article>().ToList();

            repo.GetTenArticles(0)
                .Returns(articlesFrom0To10);
            repo.GetTenArticles(10)
                .Returns(articlesFrom10To20);
            repo.GetArticlesCount()
                .Returns(20);

            sut.GetTenArticlesFromIndex(0)
                .ShouldAllBeEquivalentTo(articlesFrom0To10);
            sut.GetTenArticlesFromIndex(10)
                .ShouldAllBeEquivalentTo(articlesFrom10To20);
        }

        [Theory]
        [ShopAutoData]
        public void get_ten_articles_from_too_big_index_should_return_last_available_articles(
            [Frozen] IArticleRepository repo,
            ArticleService sut,
            List<Article> articles)
        {
            var count = articles.Count;

            repo.GetTenArticles(0)
                .Returns(articles);
            repo.GetArticlesCount()
                .Returns(count);
            
            sut.GetTenArticlesFromIndex(count + 10)
                .ShouldAllBeEquivalentTo(articles);
        }
        
    }
}
