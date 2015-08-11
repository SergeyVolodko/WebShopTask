using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Xunit.Extensions;

namespace Shop.Tests.Services
{
    public class UserServiceTests
    {
        // TODO: how to extract service and repo initializations to constructor?

        [Theory]
        [ShopAutoData]
        public void register_nonexisting_user_invoke_repository_create_user(
            [Frozen]IUserRepository repo,
            UserService sut,
            User newUser)
        {
            repo.UserExists(newUser.Login)
                .Returns(false);

            sut.RegisterUser(newUser);
            
            repo.Received()
                .CreateUser(newUser);
        }

        [Theory]
        [ShopAutoData]
        public void register_user_invoke_repository_user_exists(
            [Frozen]IUserRepository repo,
            UserService sut,
            User newUser)
        {
            sut.RegisterUser(newUser);

            repo.Received()
                .UserExists(newUser.Login);
        }

        [Theory]
        [ShopAutoData]
        public void register_user_should_return_status_ok(
            [Frozen] IUserRepository repo,
            UserService sut,
            User newUser)
        {
            repo.UserExists(newUser.Login)
                .Returns(false);

            sut.RegisterUser(newUser)
                .ShouldBeEquivalentTo(ServiceStatus.Ok);
        }

        [Theory]
        [ShopAutoData]
        public void register_existing_user_should_return_conflict(
            [Frozen] IUserRepository repo,
            UserService sut,
            User newUser)
        {
            repo.UserExists(newUser.Login)
                .Returns(true);

            sut.RegisterUser(newUser)
                .Should()
                .Be(ServiceStatus.Conflict);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_user_should_invoke_repository_user_exists(
            [Frozen] IUserRepository repo,
            UserService sut,
            User user)
        {
            sut.LoginUser(user.Login, user.Password);

            repo.Received()
                .UserExists(user.Login);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_existing_user_should_invoke_repository_get_user_by_login_and_pass(
            [Frozen] IUserRepository repo,
            UserService sut,
            User user)
        {
            repo.UserExists(user.Login)
                .Returns(true);

            sut.LoginUser(user.Login, user.Password);

            repo.Received()
                .GetUserByLoginAndPassword(user.Login, user.Password);
        }
        
        [Theory]
        [ShopAutoData]
        public void authonticate_user_with_not_existing_login_should_return_notauthorized_user(
            [Frozen] IUserRepository repo,
            UserService sut,
            User user)
        {
            repo.UserExists(user.Login)
                .Returns(false);

            sut.LoginUser(user.Login, user.Password)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }

        [Theory]
        [ShopAutoData]
        public void authonticate_user_with_wrong_password_should_return_notauthorized_user(
            [Frozen] IUserRepository repo,
            UserService sut,
            User user)
        {
            repo.UserExists(user.Login)
                .Returns(true);

            repo.GetUserByLoginAndPassword(user.Login, user.Password)
                .Returns(x => null);

            sut.LoginUser(user.Login, user.Password)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }

        [Theory]
        [ShopAutoData]
        public void authonticate_user_corret_creds_should_return_that_user(
            [Frozen] IUserRepository repo,
            UserService sut,
            User user)
        {
            repo.UserExists(user.Login)
                .Returns(true);

            repo.GetUserByLoginAndPassword(user.Login, user.Password)
                .Returns(user);

            sut.LoginUser(user.Login, user.Password)
                .Should()
                .Be(user);
        }

    }
}
