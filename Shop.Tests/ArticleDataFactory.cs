using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Shop.Domain.Entities;

namespace Shop.Tests
{
    public class ArticleDataFactory
    {
        private readonly Fixture fixture;

        public ArticleDataFactory()
        {
            fixture = new Fixture();
        }

        public Article CreateArticle()
        {
            return
                fixture.Build<Article>()
                    .Without(a => a.Id)
                    .WithAutoProperties()
                    .Create();
        }

        public List<Article> CreateArticlesList(int count)
        {
            fixture.RepeatCount = count;
            var articles = fixture.Build<Article>()
                .Without(a => a.Id)
                .WithAutoProperties()
                .CreateMany().ToList();

            return articles;
        }

        public List<Article> CreateManyArticles()
        {
            return CreateArticlesList(3);
        }

    }
}
