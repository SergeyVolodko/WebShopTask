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

        public Prdouct CreateProduct()
        {
            return
                fixture.Build<Prdouct>()
                    .Without(a => a.Id)
                    .WithAutoProperties()
                    .Create();
        }

        public List<Prdouct> CreateProductsList(int count)
        {
            fixture.RepeatCount = count;
            var products = fixture.Build<Prdouct>()
                .Without(a => a.Id)
                .WithAutoProperties()
                .CreateMany().ToList();

            return products;
        }

        public List<Prdouct> CreateManyProducts()
        {
            return CreateProductsList(3);
        }

    }
}
