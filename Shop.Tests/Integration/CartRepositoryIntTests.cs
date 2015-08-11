using System;
using FluentAssertions;
using Ninject;
using NSubstitute;
using Shop.Domain.Repositories;
using Shop.Site;
using Xunit;

namespace Shop.Tests.Integration
{
    public class CartRepositoryIntTests
    {
        [Fact]
        public void save_new_returns_cart_with_new_id()
        {
            var cart = new CartDataBuilder()
                .WithEmptyId()
                .Build();
            var sut = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<ICartRepository>();

            var actual = sut.Save(cart);

            actual.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void get_by_id_returns_cart_with_corresponding_id()
        {
            var cart = new CartDataBuilder()
                .WithEmptyId()
                .Build();

            var sut = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<ICartRepository>();

            var savedCart = sut.Save(cart);

            var actual = sut.GetCartById(savedCart.Id.Value);

            actual
                .ShouldBeEquivalentTo(savedCart);
        }
        
        [Fact]
        public void get_by_not_existing_id_returns_null()
        {
            var sut = new Global(Consts.TEST_APP_DATA)
                    .GetKernel().Get<ICartRepository>();
            
            var actual = sut.GetCartById(Arg.Any<Guid>());

            actual
                .Should()
                .BeNull();
        }
    }
}
