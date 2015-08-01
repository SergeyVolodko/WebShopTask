using System;
using System.Linq.Expressions;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Xunit;
using Xunit.Extensions;
using NHibernate;

namespace Shop.Tests
{
    public class NhibUserRepositoryTests
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
            NHibUserRepository repository,
            UserModel newUser)
        {
            repository.CreateUser(newUser);
            
            session.Received()
                .Save(newUser);
        } 
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_session_query_over(
            [Frozen]ISession session,
            NHibUserRepository repository,
            string login)
        {
            repository.UserExists(login);
            session.Received().QueryOver<UserModel>();
        }
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_proper_query_to_session(
            [Frozen]ISession session,
            NHibUserRepository repository,
            UserModel user1,
            UserModel user2)
        {
            //arrange
            Expression<Func<UserModel, bool>> actualFilter = null;

            session.QueryOver<UserModel>()
                .Where(Arg.Do<Expression<Func<UserModel, bool>>>(filter => actualFilter = filter));
                
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
            NHibUserRepository repository,
            string login,
            string password)
        {
            repository.GetUserByLoginAndPassword(login, password);

            session.Received().QueryOver<UserModel>();
        }

        [Theory]
        [ShopAutoData]
        public void get_user_by_login_and_password_invoke_proper_query_to_session(
           [Frozen]ISession session,
           NHibUserRepository repository,
           UserModel user1,
           UserModel user2)
        {
            //arrange
            Expression<Func<UserModel, bool>> actualFilter = null;

            session.QueryOver<UserModel>()
                .Where(Arg.Do<Expression<Func<UserModel, bool>>>(filter => actualFilter = filter));

            //act
            repository.GetUserByLoginAndPassword(user1.Login, user1.Password);

            //assert
            var compiledActualFilter = actualFilter.Compile();

            compiledActualFilter.Invoke(user1).Should().Be(true);
            compiledActualFilter.Invoke(user2).Should().Be(false);
        }
    }
}
