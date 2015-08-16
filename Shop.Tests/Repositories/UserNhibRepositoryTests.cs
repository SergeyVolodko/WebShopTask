using FluentAssertions;
using NHibernate;
using NSubstitute;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests.Repositories
{
    public class UserNhibRepositoryTests
    {
        private readonly UserNHibRepository sut;
        private readonly ISession session;

        public UserNhibRepositoryTests()
        {
            session = Substitute.For<ISession>();
            sut = new UserNHibRepository(session);
        }

        [Fact]
        public void fact()
        {
            1.Should().Be(1);
        }

        [Theory]
        [ShopAutoData]
        public void create_user_invoke_session_save(
            User newUser)
        {
            sut.CreateUser(newUser);
            
            session.Received()
                .Save(newUser);
        } 
        
        [Theory]
        [ShopAutoData]
        public void user_exists_invoke_session_query_over(
            string login)
        {
            sut.UserExists(login);

            session
                .Received()
                .QueryOver<User>();
        }

        [Theory]
        [ShopAutoData]
        public void get_user_by_login_and_password_invoke_session_query_over(
            string login,
            string password)
        {
            sut.GetUserByLoginAndPassword(login, password);

            session.Received().QueryOver<User>();
        }
    }
}
