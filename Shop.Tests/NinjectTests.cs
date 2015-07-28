using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ninject;
using Shop.Domain;
using Shop.Site;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests
{
    
    public class NinjectTests
    {
        [Fact]
        public void test_ninject()
        {
            var repo = new Global().GetKernel().Get<IUserRepository>();
            repo.Should().NotBeNull();
        }

        public static IEnumerable<object[]> ControllerTypes {
            get
            {
                return typeof (Global).Assembly
                    .GetTypes()
                    .Where(t => t.Name.EndsWith("Controller"))
                    .Select(t => new object[] {t});
            }
        }

        public static IEnumerable<object[]> ServiceTypes
        {
            get
            {
                return typeof (UserService).Assembly
                    .GetTypes()
                    .Where(t => t.IsInterface && t.Name.EndsWith("Service"))
                    .Select(t => new object[] {t});
            }
        } 
        
        public static IEnumerable<object[]> RepositoryTypes
        {
            get
            {
                return typeof (UserService).Assembly
                    .GetTypes()
                    .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
                    .Select(t => new object[] {t});
            }
        }

        [Theory]
        [PropertyData("RepositoryTypes")]
        [PropertyData("ServiceTypes")]
        [PropertyData("ControllerTypes")]
        public void should_return_all_repositories_services_and_controllers(Type controllerType)
        {
            var kernel = new Global().GetKernel();

            kernel.Get(controllerType)
                .Should().NotBeNull();
        }

    }
}
