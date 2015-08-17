using FluentAssertions;
using NSubstitute;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserRepository repository;
        private readonly IUserService sut;

        public UserServiceTests()
        {
            repository = Substitute.For<IUserRepository>();
            sut = new UserService(repository);
        }

        [Theory]
        [ShopAutoData]
        public void register_nonexisting_user_invoke_repository_create_user(
            User newUser)
        {
            repository.UserExists(newUser.Login)
                .Returns(false);

            sut.RegisterUser(newUser);

            repository.Received()
                .CreateUser(newUser);
        }

        [Theory]
        [ShopAutoData]
        public void register_user_invoke_repository_user_exists(
            User newUser)
        {
            sut.RegisterUser(newUser);

            repository.Received()
                      .UserExists(newUser.Login);
        }

        [Theory]
        [ShopAutoData]
        public void register_user_should_return_status_ok(
            User newUser)
        {
            repository.UserExists(newUser.Login)
                      .Returns(false);

            sut.RegisterUser(newUser)
                .ShouldBeEquivalentTo(ServiceStatus.Ok);
        }

        [Theory]
        [ShopAutoData]
        public void register_existing_user_should_return_conflict(
            User newUser)
        {
            repository.UserExists(newUser.Login)
                      .Returns(true);

            sut.RegisterUser(newUser)
                .Should()
                .Be(ServiceStatus.Conflict);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_user_should_invoke_repository_user_exists(
            User user)
        {
            sut.LoginUser(user.Login, user.Password);

            repository.Received()
                      .UserExists(user.Login);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_existing_user_should_invoke_repository_get_user_by_login_and_pass(
            User user)
        {
            repository.UserExists(user.Login)
                      .Returns(true);

            sut.LoginUser(user.Login, user.Password);

            repository.Received()
                      .GetUserByLoginAndPassword(user.Login, user.Password);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_user_with_not_existing_login_should_return_notauthorized_user(
            User user)
        {
            repository.UserExists(user.Login)
                      .Returns(false);

            sut.LoginUser(user.Login, user.Password)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }

        [Theory]
        [ShopAutoData]
        public void authonticate_user_with_wrong_password_should_return_notauthorized_user(
            User user)
        {
            repository.UserExists(user.Login)
                      .Returns(true);

            repository.GetUserByLoginAndPassword(user.Login, user.Password)
                      .Returns(x => null);

            sut.LoginUser(user.Login, user.Password)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }

        [Theory]
        [ShopAutoData]
        public void authonticate_user_corret_creds_should_return_that_user(
            User user)
        {
            repository.UserExists(user.Login)
                      .Returns(true);

            repository.GetUserByLoginAndPassword(user.Login, user.Password)
                      .Returns(user);

            sut.LoginUser(user.Login, user.Password)
               .Should()
               .Be(user);
        }

    }
}
