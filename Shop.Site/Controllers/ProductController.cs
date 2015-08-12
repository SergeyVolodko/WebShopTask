using System.Collections.Generic;
using System.Web.Http;
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

        public List<Prdouct> Get()
        {
            return productService.GetAllProducts();
        }

        public List<Prdouct> GetPageProducts(int pageNumber)
        {
            // TODO: isn't it a mixing of responisbilities if use service.getPageProducts ?
            var startIndex = pageNumber * 10 - 10;
            return productService.GetTenProductsFromIndex(startIndex);
        }
    }
}
