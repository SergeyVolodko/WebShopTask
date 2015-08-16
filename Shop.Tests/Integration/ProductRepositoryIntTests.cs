using FluentAssertions;
using Ninject;
using Shop.Domain.Repositories;
using Shop.Site;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class ProductRepositoryIntTests
    {
        //[Theory]
        //[ShopAutoData]
        //public void get_all_articles_from_empty_storage_returns_empty_list(
        //    string randomName)
        //{
        //    var appData = Consts.TEST_APP_DATA;
        //    appData.ArticlesXmlPath = randomName + ".xml";

        //    var repository = new Global(Consts.TEST_APP_DATA)
        //            .GetKernel().Get<IProductRepository>();

        //    repository.GetAll()
        //        .ShouldAllBeEquivalentTo(new List<Product>());
        //}

        [Fact]
        public void get_all_products_from_not_empty_storage_returns_list_of_products()
        {
            var prdoucts = new ProductDataFactory()
                .CreateManyProducts();

            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IProductRepository>();

            repository.Save(prdoucts);

            repository.GetAll()
                .ShouldAllBeEquivalentTo(prdoucts, o => o.Excluding(x => x.Id));
        }
        
        [Fact]
        public void get_10_products_from_not_empty_storage_returns_proper_list()
        {
            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IProductRepository>();

            var products = new ProductDataFactory()
                            .CreateProductsList(20);

            repository.Save(products);

            var expected = products.GetRange(0, 10);

            repository.GetTenProducts(0)
                .Count
                .Should().Be(10);
            repository.GetTenProducts(0)
                .ShouldAllBeEquivalentTo(expected, o => o.Excluding(x => x.Id));
        }

        [Theory]
        [ShopAutoData]
        public void get_products_count_returns_number_of_all_stored_products(
            int productsCount)
        {
            var repository = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<IProductRepository>();

            var products = new ProductDataFactory()
                            .CreateProductsList(productsCount);

            repository.Save(products);

            repository.GetProductsCount()
                .Should()
                .Be(productsCount);
        }

    }
}
