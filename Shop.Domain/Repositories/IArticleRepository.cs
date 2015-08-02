
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        void Save(List<Article> articles);
    }
}
