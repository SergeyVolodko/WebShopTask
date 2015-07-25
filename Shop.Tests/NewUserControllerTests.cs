﻿using System.Net;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain;
using Shop.Site.Controllers;
using Xunit.Extensions;

namespace Shop.Tests
{
    public class NewUserControllerTests
    {
        [Theory]
        [ShopAutoDataAttribute]
        public void register_user_should_return_http_status_created(
           [Frozen] IUserService service,
           // IUserController sut,
            UserModel newUser)
        {
            var sut = new UserController(service);

            ((HttpStatusCode)sut.Post(newUser))
                .Should()
                .Be(HttpStatusCode.Created);
        }
        
        [Theory]
        [ShopAutoDataAttribute]
        public void register_existing_user_should_return_http_status_conflict(
            [Frozen] IUserService service,
            //IUserController sut,
            UserModel newUser)
        {
            var sut = new UserController(service);

            service.RegisterUser(newUser)
                .Returns(ServiceStatus.Conflict);

            ((HttpStatusCode)sut.Post(newUser))
                .Should()
                .Be(HttpStatusCode.Conflict);
        }

        [Theory]
        [ShopAutoDataAttribute]
        public void register_user_should_invoke_service_new_user(
            [Frozen] IUserService service,
            //UserController sut,
            UserModel newUser)
        {

            var sut = new UserController(service);
            
            sut.Post(newUser);
            service.Received()
                .RegisterUser(newUser);
        }
    }


}