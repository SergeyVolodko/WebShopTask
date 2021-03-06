﻿using FluentAssertions;
using Ninject;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Shop.Domain.Utils;
using Shop.Site;
using Shop.Tests.DataCreation;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class CartServiceIntTests
    {
        private readonly IKernel kernel;
        private readonly IProductRepository productRepository;

        public CartServiceIntTests()
        {
            kernel = new Global(Consts.TEST_APP_DATA)
                .GetKernel();
            productRepository = kernel.Get<IProductRepository>();
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_empty_cart_returns_new_cart_containing_product(
            Product product)
        {
            product = productRepository.Save(product);

            var sut = kernel.Get<ICartService>();

            var actual = sut.AddProductToCart(null, product.Id.Value);

            actual
                .Should()
                .NotBeNull();
            actual
                .Items
                .Should()
                .Contain(item => item.Product.Id == product.Id);
        }

        [Fact]
        public void add_product_to_existing_cart_returns_cart_containing_product()
        {
            var products = new ProductDataFactory()
                .CreateProductsList(2);
           
            productRepository.Save(products);

            var sut = kernel.Get<ICartService>();

            var cart = sut.AddProductToCart(null, products[0].Id.Value);

            var actual = sut.AddProductToCart(cart.Id, products[1].Id.Value);

            actual.Id
                .Should()
                .Be(cart.Id);
            actual
                .Items
                .Should()
                .Contain(item => item.Product.Id == products[1].Id);
        }

        [Theory]
        [ShopAutoData]
        public void add_product_to_cart_twice_should_increase_cart_item_quantity(
            Product product)
        {
            product = productRepository.Save(product);

            var productId = product.Id.Value;

            var sut = kernel.Get<ICartService>();

            var cart = sut.AddProductToCart(null, productId);

            var actual = sut.AddProductToCart(cart.Id, productId);

            actual.Items[0]
                .Quantity
                .Should()
                .Be(2);
        }

        [Theory]
        [ShopAutoData]
        public void get_total_for_not_empty_cart_returns_correct_summary()
        {
            var products = new ProductDataFactory()
                .CreateProductsListWithFixedPrice(3, 50);

            productRepository.Save(products);

            var sut = kernel.Get<ICartService>();

            var cart = sut.AddProductToCart(null, products[0].Id.Value);
            foreach (var product in products)
            {
                sut.AddProductToCart(cart.Id, product.Id.Value);
            }

            var expected = new TotalSummary
            {
               VATRate = 0.2,
               VAT = 40,
               TotalExcludingVAT = 200,
               TotalIncludingVAT = 240
            };

            sut.GetTotal(cart.Id.Value)
                .ShouldBeEquivalentTo(expected);
        }
    }
}
