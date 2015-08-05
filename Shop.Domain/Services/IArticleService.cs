using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Services
{
    public interface IArticleService
    {
        List<Article> GetAllArticles();
        List<Article> GetTenArticlesFromIndex(int startIndex);
    }
}
