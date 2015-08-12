using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class ProductServiceTests
    {
        [Theory] 
        [ShopAutoData]
        public void get_all_products_returns_empty_list_if_no_products_stored(
            [Frozen] IProductRepository repo,
            ProductService sut)
        {
            repo.GetAll()
                .Returns(new List<Prdouct>());

            sut.GetAllProducts()
                .ShouldAllBeEquivalentTo(new List<Prdouct>());
        }
        
        [Theory]
        [ShopAutoData]
        public void get_all_products_returns_list_of_all_stored_products(
            [Frozen] IProductRepository repo,
            ProductService sut)
        {
            var products = new ProductDataFactory()
                .CreateManyProducts();

            repo.GetAll()
                .Returns(products);

            sut.GetAllProducts()
                .ShouldAllBeEquivalentTo(products);
        }

        [Theory]
        [ShopAutoData]
        public void get_ten_products_returns_proper_list_of_10_stored_products(
            [Frozen] IProductRepository repo,
            ProductService sut)
        {
            var dataFactory = new ProductDataFactory();
            var productsFrom0To10 = dataFactory
                                    .CreateProductsList(10);
            var productsFrom10To20 = dataFactory
                                    .CreateProductsList(10);

            repo.GetTenProducts(0)
                .Returns(productsFrom0To10);
            repo.GetTenProducts(10)
                .Returns(productsFrom10To20);
            repo.GetProductsCount()
                .Returns(20);

            sut.GetTenProductsFromIndex(0)
                .ShouldAllBeEquivalentTo(productsFrom0To10);
            sut.GetTenProductsFromIndex(10)
                .ShouldAllBeEquivalentTo(productsFrom10To20);
        }

        [Theory]
        [ShopAutoData]
        public void get_ten_products_from_too_big_index_should_return_last_available_products(
            [Frozen] IProductRepository repo,
            ProductService sut)
        {
            var products = new ProductDataFactory()
                .CreateManyProducts();

            var count = products.Count;

            repo.GetTenProducts(0)
                .Returns(products);
            repo.GetProductsCount()
                .Returns(count);
            
            sut.GetTenProductsFromIndex(count + 10)
                .ShouldAllBeEquivalentTo(products);
        }
        
    }
}
