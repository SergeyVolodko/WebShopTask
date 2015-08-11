using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Common;
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
    public class UserNhibRepositoryTests
    {

        // TODO: how to check LINQ queries?

        [Fact]
        public void fact()
        {
            1.Should().Be(1);
        }

        [Theory]
        [ShopAutoData]
        public void create_user_invoke_session_save(
            [Frozen]ISession session,
            UserNHibRepository sut,
            User newUser)
        {
            sut.CreateUser(newUser);
            
            session.Received()
                .Save(Arg.Any<UserDto>());
        } 
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_session_query_over(
            [Frozen]ISession session,
            UserNHibRepository sut,
            string login)
        {
            sut.UserExists(login);

            session
                .Received()
                .QueryOver<UserDto>();
        }
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_proper_query_to_session(
            [Frozen]ISession session,
            UserNHibRepository sut,
            User user1,
            User user2)
        {
            //arrange
            Expression<Func<UserDto, bool>> actualFilter = null;

            session.QueryOver<UserDto>()
                .Where(Arg.Do<Expression<Func<UserDto, bool>>>(filter => actualFilter = filter));
                
            //act
            sut.UserExists(user1.Login);
  
            //assert
            var compiledActualFilter = actualFilter.Compile();

            compiledActualFilter.Invoke(user1).Should().Be(true);
            compiledActualFilter.Invoke(user2).Should().Be(false);
        }

        [Theory]
        [ShopAutoData]
        public void get_user_by_login_and_password_invoke_session_query_over(
            [Frozen]ISession session,
            UserNHibRepository repository,
            string login,
            string password)
        {
            repository.GetUserByLoginAndPassword(login, password);

            session.Received().QueryOver<UserDto>();
        }

        [Theory]
        [ShopAutoData]
        public void get_user_by_login_and_password_invoke_proper_query_to_session(
           [Frozen]ISession session,
           UserNHibRepository repository,
           User user1,
           User user2)
        {
            //arrange
            Expression<Func<UserDto, bool>> actualFilter = null;

            session.QueryOver<UserDto>()
                .Where(Arg.Do<Expression<Func<UserDto, bool>>>(filter => actualFilter = filter));

            //act
            repository.GetUserByLoginAndPassword(user1.Login, user1.Password);

            //assert
            var compiledActualFilter = actualFilter.Compile();

            compiledActualFilter.Invoke(user1).Should().Be(true);
            compiledActualFilter.Invoke(user2).Should().Be(false);
        }
    }
}
