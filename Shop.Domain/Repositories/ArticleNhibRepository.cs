using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Repositories
{
    public class ArticleNhibRepository: IArticleRepository
    {
        private readonly ISession session;

        public ArticleNhibRepository(ISession session)
        {
            this.session = session;
        }

        public List<Article> GetAll()
        {
            var articles = session.QueryOver<ArticleDto>().List();
            return articles
                .Select(a => (Article)a)
                .ToList();
        }

        public List<Article> GetTenArticles(int startIndex)
        {
            var articles = session.QueryOver<ArticleDto>().List();
            return articles
                .Select(a => (Article)a)
                .ToList()
                .GetRange(startIndex, 10);
        }

        public int GetArticlesCount()
        {
            return session.QueryOver<ArticleDto>().RowCount();
        }

        public void Save(List<Article> articles)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var article in articles)
                {
                    session.Save((ArticleDto)article);
                }
                
                transaction.Commit();
            }
        }

        public Article Save(Article article)
        {
            var dto = (ArticleDto)article;
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(dto);
                transaction.Commit();
            }
            return dto == null ? null
                   : (Article)dto;
        }
    }
}
