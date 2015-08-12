using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Services;
using Shop.Site.Controllers;
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

            service.GetTenProductsFromIndex(0)
                .Returns(products);

            sut.GetPageProducts(1)
                .ShouldAllBeEquivalentTo(products);
        }
        
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 10)]
        [InlineData(3, 20)]
        public void get_page_products_for_specific_page_should_call_service_with_proper_parameter(
            int pageNumber,
            int startIndex)
        {
            var service = Substitute.For<IProductService>();
            var sut = new ProductController(service);
            
            sut.GetPageProducts(pageNumber);

            service.Received()
                .GetTenProductsFromIndex(startIndex);
        }
    }
}
