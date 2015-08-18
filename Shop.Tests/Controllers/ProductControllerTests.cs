using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Site.Controllers;
using Shop.Tests.DataCreation;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public void fact()
        {
            1.Should().Be(1);
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_returns_list_of_all_stored_products(
            [Frozen] IProductService service,
            ProductController sut)
        {
            var products = new ProductDataFactory()
                .CreateManyProducts();
            
            service.GetAllProducts()
                .Returns(products);

            sut.Get()
                .ShouldAllBeEquivalentTo(products);
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_invokes_product_service_get_all(
            [Frozen] IProductService service,
            ProductController sut)
        {
            sut.Get();

            service.Received()
                .GetAllProducts();
        }

        [Theory]
        [ShopControllerAutoData]
        public void get_page_products_returns_list_of_10_products(
            [Frozen] IProductService service,
            ProductController sut)
        {
            var products = new ProductDataFactory()
                .CreateProductsList(10);

            service.GetProductsForPage(1)
                .Returns(products);

            sut.GetPageProducts(1)
                .ShouldAllBeEquivalentTo(products);
        }
        
        
    }
}
