using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly IProductRepository repository;
        private readonly ProductService sut;

        public ProductServiceTests()
        {
            repository = Substitute.For<IProductRepository>();
            sut = new ProductService(repository);
        }

        [Fact]
        public void get_all_products_returns_empty_list_if_no_products_stored()
        {
            repository.GetAll()
                .Returns(new List<Product>());

            sut.GetAllProducts()
               .ShouldAllBeEquivalentTo(new List<Product>());
        }
        
        [Fact]
        public void get_all_products_returns_list_of_all_stored_products()
        {
            var products = new ProductDataFactory()
                .CreateManyProducts();

            repository.GetAll()
                .Returns(products);

            sut.GetAllProducts()
                .ShouldAllBeEquivalentTo(products);
        }

        [Fact]
        public void get_ten_products_returns_proper_list_of_10_stored_products()
        {
            var dataFactory = new ProductDataFactory();
            var productsFrom0To10 = dataFactory
                                    .CreateProductsList(10);
            var productsFrom10To20 = dataFactory
                                    .CreateProductsList(10);

            repository.GetTenProducts(0)
                .Returns(productsFrom0To10);
            repository.GetTenProducts(10)
                .Returns(productsFrom10To20);
            repository.GetProductsCount()
                .Returns(20);

            sut.GetProductsForPage(1)
                .ShouldAllBeEquivalentTo(productsFrom0To10);
            sut.GetProductsForPage(2)
                .ShouldAllBeEquivalentTo(productsFrom10To20);
        }

        [Fact]
        public void get_ten_products_for_too_big_page_number_should_return_last_available_products()
        {
            var products = new ProductDataFactory()
                .CreateProductsList(9);

            var count = products.Count;

            repository.GetTenProducts(0)
                .Returns(products);
            repository.GetProductsCount()
                .Returns(count);
            
            sut.GetProductsForPage(2)
                .ShouldAllBeEquivalentTo(products);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 10)]
        [InlineData(3, 20)]
        public void get_page_products_for_specific_page_should_call_repository_with_proper_parameter(
            int pageNumber,
            int startIndex)
        {
            repository.GetProductsCount()
                .Returns(30);

            sut.GetProductsForPage(pageNumber);
            
            repository.Received()
                .GetTenProducts(startIndex);
        }
    }
}
