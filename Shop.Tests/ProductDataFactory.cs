using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Shop.Domain.Entities;

namespace Shop.Tests
{
    public class ProductDataFactory
    {
        private readonly Fixture fixture;

        public ProductDataFactory()
        {
            fixture = new Fixture();
        }

        public List<Product> CreateProductsList(int count)
        {
            fixture.RepeatCount = count;
            var products = fixture.Build<Product>()
                .Without(a => a.Id)
                .WithAutoProperties()
                .CreateMany().ToList();

            return products;
        }
        
        public List<Product> CreateProductsListWithFixedPrice(int count, double price)
        {
            fixture.RepeatCount = count;
            var products = fixture.Build<Product>()
                .Without(a => a.Id)
                .With(p => p.Price, price)
                .WithAutoProperties()
                .CreateMany().ToList();

            return products;
        }
        
        public List<Product> CreateSavedProductsList(int count)
        {
            fixture.RepeatCount = count;
            var products = fixture.Build<Product>()
                .WithAutoProperties()
                .CreateMany().ToList();

            return products;
        }

        public List<Product> CreateManyProducts()
        {
            return CreateProductsList(3);
        }

    }
}
