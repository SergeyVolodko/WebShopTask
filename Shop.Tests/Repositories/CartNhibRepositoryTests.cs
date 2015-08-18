using System;
using NHibernate;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
using Shop.Domain.Repositories.Impl;
using Shop.Tests.DataCreation;
using Xunit.Extensions;

namespace Shop.Tests.Repositories
{
    public class CartNhibRepositoryTests
    {
        [Theory]
        [ShopAutoData]
        public void save_invoke_session_save(
            [Frozen]ISession session,
            CartNHibRepository sut)
        {
            var cart = new CartDataBuilder()
                .Build();

            sut.Save(cart);

            session.Received()
                .Save(cart);
        }
 
        [Theory]
        [ShopAutoData]
        public void get_cart_by_id_invoke_session_query_over(
            [Frozen]ISession session,
            CartNHibRepository sut,
            Guid cartId)
        {
            sut.GetCartById(cartId);
            
            session
                .Received()
                .QueryOver<Cart>();
        }
    }
}
