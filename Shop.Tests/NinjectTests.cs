using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ninject;
using Shop.Site;
using Shop.Site.Controllers;
using Xunit;
using Xunit.Extensions;

namespace Shop.Tests
{
    
    public class NinjectTests
    {
        [Fact]
        public void test_ninject()
        {
            new Global().GetKernel().Get<UserController>().Should().NotBeNull();

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

        [Theory]
        [PropertyData("ControllerTypes")]
        public void should_return_all_controllers(Type controllerType)
        {
            var kernel = new Global().GetKernel();

            kernel.Get(controllerType).Should().NotBeNull();
        }
    }
}
