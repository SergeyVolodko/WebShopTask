using FluentAssertions;
using Ninject;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Site;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class NHibUserRepositoryIntTests
    {
        private readonly IUserRepository repository;
        
        public NHibUserRepositoryIntTests()
        {
            repository = new Global(Consts.TEST_APP_DATA)
                .GetKernel().Get<IUserRepository>();
        }

        [Theory]
        [ShopAutoData]
        public void check_if_user_exists_in_empty_base_returns_false(
            string notExistingLogin)
        {
            repository.UserExists(notExistingLogin)
                      .Should().Be(false);
        }
        
        [Theory]
        [ShopAutoData]
        public void after_user_is_added_user_exists_check_returns_true(
            User newUser)
        {
            repository.CreateUser(newUser);

            repository.UserExists(newUser.Login)
                .Should().Be(true);
        }

        [Theory]
        [ShopAutoData]
        public void get_existing_user_by_login_and_pass_returns_that_user(
            User existingUser)
        {
            repository.CreateUser(existingUser);

            repository.GetUserByLoginAndPassword(existingUser.Login, existingUser.Password)
                .Should()
                .Be(existingUser);
        }
        
        [Theory]
        [ShopAutoData]
        public void get_user_by_login_and_wrong_pass_returns_null(
            User existingUser,
            string wrongPassword)
        {
            repository.CreateUser(existingUser);

            repository.GetUserByLoginAndPassword(existingUser.Login, wrongPassword)
                .Should()
                .BeNull();
        }

        [Theory]
        [ShopAutoData]
        public void get_user_by_wrong_login_and_pass_returns_null(
            User existingUser,
            string wrongLogin)
        {
            repository.CreateUser(existingUser);

            repository.GetUserByLoginAndPassword(wrongLogin, existingUser.Password)
                .Should()
                .BeNull();
        }
    }
}
