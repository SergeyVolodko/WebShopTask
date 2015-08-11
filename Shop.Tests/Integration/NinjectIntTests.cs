using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ninject;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Shop.Site;
using Xunit.Extensions;

namespace Shop.Tests.Integration
{
    public class NinjectIntTests
    {
        [Theory]
        [PropertyData("RepositoryTypes")]
        [PropertyData("ServiceTypes")]
        [PropertyData("ControllerTypes")]
        [PropertyData("FactoryTypes")]
        public void should_return_all_repositories_services_and_controllers(Type type)
        {
            var kernel = new Global(Consts.TEST_APP_DATA).GetKernel();

            kernel.Get(type)
                .Should().NotBeNull();
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
                return typeof (IUserRepository).Assembly
                    .GetTypes()
                    .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
                    .Select(t => new object[] {t});
            }
        }
        
        public static IEnumerable<object[]> FactoryTypes
        {
            get
            {
                return typeof (IUserRepository).Assembly
                    .GetTypes()
                    .Where(t => t.IsInterface && t.Name.EndsWith("Factory"))
                    .Select(t => new object[] {t});
            }
        }
    }
}
