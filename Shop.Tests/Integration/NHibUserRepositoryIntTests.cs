using FluentAssertions;
using Ninject;
using Shop.Domain;
using Shop.Site;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class NHibUserRepositoryIntTests
    {
        private readonly IUserRepository repository;

        public NHibUserRepositoryIntTests()
        {
            repository = new Global(Consts.TEST_DB_PATH)
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
            UserModel newUser)
        {
            repository.CreateUser(newUser);

            repository.UserExists(newUser.Login)
                .Should().Be(true);
        }
    }
}
