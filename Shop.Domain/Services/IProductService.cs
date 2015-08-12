using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Services
{
    public interface IProductService
    {
        List<Prdouct> GetAllProducts();
        List<Prdouct> GetTenProductsFromIndex(int startIndex);
    }
}
