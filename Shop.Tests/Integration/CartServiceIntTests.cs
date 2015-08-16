using FluentAssertions;
using Ninject;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Shop.Site;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class CartServiceIntTests
    {
        private readonly IKernel kernel;
        private readonly IProductRepository repository;

        public CartServiceIntTests()
        {
            kernel = new Global(Consts.TEST_APP_DATA)
                .GetKernel();
            repository = kernel.Get<IProductRepository>();
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_returns_new_cart_containing_product(
            Product product)
        {
            product = repository.Save(product);
            
            var sut = kernel.Get<ICartService>();

            var actual = sut.AddProductToCart(null, product);

            actual
                .Should()
                .NotBeNull();
            actual
                .Products[0]
                .ShouldBeEquivalentTo(product);
        }

        // TODO: how to resolve mappings in the right way? Is this test correct?
        [Fact]
        public void add_product_to_existing_cart_returns_cart_containing_product()
        {
            var products = new ProductDataFactory()
                .CreateProductsList(2);
            
            var sut = kernel.Get<ICartService>();

            var cart = sut.AddProductToCart(null, products[0]);

            var actual = sut.AddProductToCart(cart.Id, products[1]);

            actual.Id
                .Should()
                .Be(cart.Id);
            actual
                .Products
                .Should()
                .Contain(products[1]);
        }
    }
}
