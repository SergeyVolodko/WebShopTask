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

        public List<Prdouct> GetAllProducts()
        {
            return repository.GetAll();
        }

        public List<Prdouct> GetTenProductsFromIndex(int startIndex)
        {
            var count = repository.GetProductsCount();
            if (startIndex > count)
            {
                int lastAvailableIndex = count / 10;
                return repository.GetTenProducts(lastAvailableIndex);
            }

            return repository.GetTenProducts(startIndex);
        }
    }
}
