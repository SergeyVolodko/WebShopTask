using FluentAssertions;
using Ninject;
using Shop.Domain.Repositories;
using Shop.Site;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class ArticleRepositoryIntTests
    {
        //[Theory]
        //[ShopAutoData]
        //public void get_all_articles_from_empty_storage_returns_empty_list(
        //    string randomName)
        //{
        //    var appData = Consts.TEST_APP_DATA;
        //    appData.ArticlesXmlPath = randomName + ".xml";

        //    var repository = new Global(Consts.TEST_APP_DATA)
        //            .GetKernel().Get<IArticleRepository>();

        //    repository.GetAll()
        //        .ShouldAllBeEquivalentTo(new List<Article>());
        //}

        [Fact]
        public void get_all_articles_from_not_empty_storage_returns_list_of_articles()
        {
            var articles = new ArticleDataFactory()
                .CreateManyArticles();

            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IArticleRepository>();

            repository.Save(articles);

            repository.GetAll()
                .ShouldAllBeEquivalentTo(articles, o => o.Excluding(x => x.Id));
        }
        
        [Fact]
        public void get_10_articles_from_not_empty_storage_returns_proper_list_of_articles()
        {
            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IArticleRepository>();

            var articles = new ArticleDataFactory()
                            .CreateArticlesList(20);

            repository.Save(articles);

            var expected = articles.GetRange(0, 10);

            repository.GetTenArticles(0)
                .Count
                .Should().Be(10);
            repository.GetTenArticles(0)
                .ShouldAllBeEquivalentTo(expected, o => o.Excluding(x => x.Id));
        }

        [Theory]
        [ShopAutoData]
        public void get_articles_count_returns_number_of_all_stored_articles(
            int articlesCount)
        {
            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IArticleRepository>();

            var articles = new ArticleDataFactory()
                            .CreateArticlesList(articlesCount);

            repository.Save(articles);

            repository.GetArticlesCount()
                .Should()
                .Be(articlesCount);
        }

    }
}
