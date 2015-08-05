
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        List<Article> GetTenArticles(int startIndex);
        int GetArticlesCount();
        void Save(List<Article> articles);
    }
}
