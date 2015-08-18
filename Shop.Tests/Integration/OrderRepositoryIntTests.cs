using FluentAssertions;
using Shop.Domain.Entities;
using Shop.Domain.Repositories.Impl;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class OrderRepositoryIntTests
    {
        [Theory]
        [ShopAutoData]
        public void save_order_returns_saved_order_with_proper_members(
            OrderNhibRepository sut,
            ShopOrder order)
        {
            var actual = sut.Save(order);

            actual.Id
                .Should()
                .NotBeEmpty();

            actual.ShouldBeEquivalentTo(order, o => o.ExcludingMissingMembers());
        }
    }
}
