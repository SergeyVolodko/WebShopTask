﻿using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;

namespace Shop.Tests
{
    public class ShopControllerAutoDataAttribute: AutoDataAttribute
    {
        public ShopControllerAutoDataAttribute()
            : base(new Fixture().Customize(new ShopControllerCustomization()))
        {
        }
    }

    internal class ShopControllerCustomization : CompositeCustomization
    {
        internal ShopControllerCustomization()
            : base(
                new HttpRequestMessageCustomization(),
                new ApiControllerCustomization(),
                new AutoNSubstituteCustomization())
        {
        }
    }

    internal class HttpRequestMessageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpRequestMessage>(c => c
                .Without(x => x.Content)
                .Do(x => x.Properties[HttpPropertyKeys.HttpConfigurationKey] =
                    new HttpConfiguration()));
        }
    }

    internal class ApiControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new Postprocessor(
                        new MethodInvoker(
                            new ModestConstructorQuery()),
                        new ApiControllerFiller()),
                    new ApiControllerSpecification()));
        }

        private class ApiControllerFiller : ISpecimenCommand
        {
            public void Execute(object specimen, ISpecimenContext context)
            {
                if (specimen == null)
                    throw new ArgumentNullException("specimen");
                if (context == null)
                    throw new ArgumentNullException("context");

                var target = specimen as ApiController;
                if (target == null)
                    throw new ArgumentException(
                        "The specimen must be an instance of ApiController.",
                        "specimen");

                target.Request =
                    (HttpRequestMessage)context.Resolve(
                        typeof(HttpRequestMessage));
            }
        }

        private class ApiControllerSpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                var requestType = request as Type;
                if (requestType == null)
                    return false;
                return typeof(ApiController).IsAssignableFrom(requestType);
            }
        }
    }
}
