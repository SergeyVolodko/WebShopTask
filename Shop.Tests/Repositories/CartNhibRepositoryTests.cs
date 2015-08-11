using System;
using System.Linq.Expressions;
using FluentAssertions;
using NHibernate;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;
using Shop.Domain.Repositories;
using Xunit;
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
                .Save(Arg.Any<CartDto>() /*cart*/);
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
                .QueryOver<CartDto>();
        }
        
        //[Theory]
        //[ShopAutoData]
        //public void get_cart_by_id_invoke_proper_query_to_session(
        //    [Frozen]ISession session,
        //    CartNHibRepository sut,
        //    Guid cartId)
        //{

        //    //arrange
        //    Expression<Func<Cart, bool>> actualFilter = null;
        //    session.QueryOver<Cart>()
        //        .Where(Arg.Do<Expression<Func<Cart, bool>>>(filter => actualFilter = filter));

        //    var expectedCart = new CartDataBuilder()
        //        .WithId(cartId)
        //        .Build();
        //    var notExpectedCart = new CartDataBuilder()
        //        .WithId(Arg.Any<Guid>())
        //        .Build();

        //    //act
        //    sut.GetCartById(cartId);

        //    //assert
        //    var compiledActualFilter = actualFilter.Compile();

        //    compiledActualFilter
        //        .Invoke(expectedCart)
        //        .Should()
        //        .Be(true);
        //    compiledActualFilter
        //        .Invoke(notExpectedCart)
        //        .Should()
        //        .Be(false);
        //}
 

    }
}
