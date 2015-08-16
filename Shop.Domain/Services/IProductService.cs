using System.Collections.Generic;
using Shop.Domain.Entities;

namespace Shop.Domain.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsForPage(int pageNumber);
    }
}
