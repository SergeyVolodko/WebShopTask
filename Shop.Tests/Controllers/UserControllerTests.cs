using System.Net;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Domain.Entities;
using Shop.Site.Controllers;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class UserControllerTests
    {
        [Theory]
        [ShopControllerAutoData]
        public void register_user_should_return_http_status_created(
            [Frozen] IUserService service,
            UserController sut,
            UserModel newUser)
        {
            ((HttpStatusCode)sut.Post(newUser))
                .Should()
                .Be(HttpStatusCode.Created);
        }
        
        [Theory]
        [ShopControllerAutoData]
        public void register_existing_user_should_return_http_status_conflict(
            [Frozen] IUserService service,
            UserController sut,
            UserModel newUser)
        {
            service.RegisterUser(newUser)
                .Returns(ServiceStatus.Conflict);

            ((HttpStatusCode)sut.Post(newUser))
                .Should()
                .Be(HttpStatusCode.Conflict);
        }

        [Theory]
        [ShopControllerAutoData]
        public void register_user_should_invoke_service_new_user(
            [Frozen] IUserService service,
            UserController sut,
            UserModel newUser)
        {
            sut.Post(newUser);
            service.Received()
                .RegisterUser(newUser);
        }
    }
    
}
