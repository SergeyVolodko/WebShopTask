using System.Collections.Generic;
using System.Web.Http;
using System.Web.UI;
using Shop.Domain.Entities;
using Shop.Domain.Services;

namespace Shop.Site.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService service)
        {
            productService = service;
        }

        public List<Product> Get()
        {
            return productService.GetAllProducts();
        }

        public List<Product> GetPageProducts(int pageNumber)
        {
            return productService.GetProductsForPage(pageNumber);
        }
    }
}
