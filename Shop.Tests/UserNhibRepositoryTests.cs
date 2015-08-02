using System;
using System.Linq.Expressions;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Xunit;
using Xunit.Extensions;
using NHibernate;

namespace Shop.Tests
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
            UserNHibRepository repository,
            User newUser)
        {
            repository.CreateUser(newUser);
            
            session.Received()
                .Save(newUser);
        } 
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_session_query_over(
            [Frozen]ISession session,
            UserNHibRepository repository,
            string login)
        {
            repository.UserExists(login);
            session.Received().QueryOver<User>();
        }
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_proper_query_to_session(
            [Frozen]ISession session,
            UserNHibRepository repository,
            User user1,
            User user2)
        {
            //arrange
            Expression<Func<User, bool>> actualFilter = null;

            session.QueryOver<User>()
                .Where(Arg.Do<Expression<Func<User, bool>>>(filter => actualFilter = filter));
                
            //act
            repository.UserExists(user1.Login);
  
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

            session.Received().QueryOver<User>();
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
            Expression<Func<User, bool>> actualFilter = null;

            session.QueryOver<User>()
                .Where(Arg.Do<Expression<Func<User, bool>>>(filter => actualFilter = filter));

            //act
            repository.GetUserByLoginAndPassword(user1.Login, user1.Password);

            //assert
            var compiledActualFilter = actualFilter.Compile();

            compiledActualFilter.Invoke(user1).Should().Be(true);
            compiledActualFilter.Invoke(user2).Should().Be(false);
        }
    }
}
