using FluentAssertions;
using Ninject;
using Shop.Domain.Repositories;
using Shop.Site;
using Shop.Tests.DataCreation;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class ProductRepositoryIntTests
    {
        private readonly IProductRepository sut;

        public ProductRepositoryIntTests()
        {
            sut = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IProductRepository>();
        }

        [Fact]
        public void get_all_products_from_not_empty_storage_returns_list_of_products()
        {
            var products = new ProductDataFactory()
                .CreateManyProducts();

            sut.Save(products);

            sut.GetAll()
                .ShouldAllBeEquivalentTo(products, o => o.Excluding(x => x.Id));
        }
        
        [Fact]
        public void get_10_products_from_not_empty_storage_returns_proper_list()
        {
            var products = new ProductDataFactory()
                            .CreateProductsList(20);

            sut.Save(products);

            var expected = products.GetRange(0, 10);

            sut.GetTenProducts(0)
                .Count
                .Should().Be(10);
            sut.GetTenProducts(0)
                .ShouldAllBeEquivalentTo(expected, o => o.Excluding(x => x.Id));
        }

        [Theory]
        [ShopAutoData]
        public void get_products_count_returns_number_of_all_stored_products(
            int productsCount)
        {
            var products = new ProductDataFactory()
                            .CreateProductsList(productsCount);

            sut.Save(products);

            sut.GetProductsCount()
                .Should()
                .Be(productsCount);
        }

        [Fact]
        public void get_by_id_returns_corresponding_product()
        {
            var products = new ProductDataFactory()
                .CreateProductsList(3);

            sut.Save(products);

            var expected = products[1];

            sut.GetById(products[1].Id.Value)
                .ShouldBeEquivalentTo(expected);
        }
    }
}
