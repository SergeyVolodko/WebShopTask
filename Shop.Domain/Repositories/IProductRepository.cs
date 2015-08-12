
using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IProductRepository
    {
        List<Prdouct> GetAll();
        List<Prdouct> GetTenProducts(int startIndex);
        int GetProductsCount();
        void Save(List<Prdouct> products);
        Prdouct Save(Prdouct prdouct);
    }
}
