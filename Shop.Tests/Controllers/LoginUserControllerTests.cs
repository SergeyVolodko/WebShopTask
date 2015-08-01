using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Site.Controllers;
using Shop.Site.Models;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class LoginUserControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void login_user_should_invoke_user_service_authonticate_user(
            [Frozen] IUserService service,
            LoginUserController sut,
            LoginData loginData)
        {
            sut.Post(loginData);

            service.Received()
                .LoginUser(loginData.Login, loginData.Password);
        }

        [Theory]
        [ShopControllerAutoData]
        public void happy_run(
            [Frozen] IUserService service,
            LoginUserController sut,
            UserModel existingUser)
        {
            service.LoginUser(existingUser.Login, existingUser.Password)
                .Returns(existingUser);

            var loginData = new LoginData
            {
                Login = existingUser.Login,
                Password = existingUser.Password
            };

            sut.Post(loginData)
                .Should()
                .Be(existingUser);
        }
        
        [Theory]
        [ShopControllerAutoData]
        public void not_existing_login_should_return_notauthorized_user(
            [Frozen] IUserService service,
            LoginUserController sut,
            string wrongLogin,
            string anyPassword)
        {
            service.LoginUser(wrongLogin, anyPassword)
                .Returns(new NotAuthorizedUser());

            var loginData = new LoginData
            {
                Login = wrongLogin,
                Password = anyPassword
            };
            
            sut.Post(loginData)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }

        [Theory]
        [ShopControllerAutoData]
        public void correct_login_but_wrong_password_should_return_notauthorized_user(
            [Frozen] IUserService service,
            LoginUserController sut,
            string existingLogin,
            string wrongPassword)
        {
            service.LoginUser(existingLogin, wrongPassword)
                .Returns(new NotAuthorizedUser());

            var loginData = new LoginData
            {
                Login = existingLogin,
                Password = wrongPassword
            };
            
            sut.Post(loginData)
                .Should()
                .BeOfType<NotAuthorizedUser>();
        }
    }
}
