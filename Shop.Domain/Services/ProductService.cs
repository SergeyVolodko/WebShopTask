using System;
using System.Collections.Generic;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        public List<Product> GetProductsForPage(int pageNumber)
        {
            var count = repository.GetProductsCount();
            var startIndex = pageNumber * 10 - 10;
            if (startIndex > count)
            {
                int lastAvailableIndex = count / 10;
                return repository.GetTenProducts(lastAvailableIndex);
            }

            return repository.GetTenProducts(startIndex);
        }
    }
}
