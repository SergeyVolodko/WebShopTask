using System.Collections.Generic;
using FluentAssertions;
using Ninject;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Site;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class ArticleXMLRepositoryIntTests
    {
        [Theory]
        [ShopAutoData]
        public void get_all_articles_from_empty_storage_returns_empty_collection(
            string randomName)
        {
            var appData = Consts.TEST_APP_DATA;
            appData.ArticlesXmlPath = randomName + ".xml";

            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IArticleRepository>();

            repository.GetAll()
                .ShouldAllBeEquivalentTo(new List<Article>());
        }

        [Theory]
        [ShopAutoData]
        public void get_all_articles_from_not_empty_storage_returns_collection_of_articles(
            List<Article> articles)
        {
            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IArticleRepository>();

            repository.Save(articles);

            repository.GetAll()
                .ShouldAllBeEquivalentTo(articles);
        }
    }
}
