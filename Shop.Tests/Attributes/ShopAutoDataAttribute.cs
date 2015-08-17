using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Shop.Domain.Entities;

namespace Shop.Tests
{
    public class ShopAutoDataAttribute: AutoDataAttribute
    {
        public ShopAutoDataAttribute()
            : base(new Fixture().Customize(new ShopAutoDataCustomization()))
        {
        }
    }

    internal class ShopAutoDataCustomization : CompositeCustomization
    {
        internal ShopAutoDataCustomization()
            : base(
                new IgnoreVirtualListMembersCustomisation(),
                new AutoConfiguredNSubstituteCustomization())
        {
        }
    }

    public class IgnoreVirtualListMembersCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreVirtualListMembers());
        }
    }

    public class IgnoreVirtualListMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var pi = request as PropertyInfo;
            if (pi == null)
            {
                return new NoSpecimen(request);
            }

            if (pi.GetGetMethod().IsVirtual
                && pi.PropertyType.Name.StartsWith("IList"))
            {
                return new OmitSpecimen();
            }

            return new NoSpecimen(request);
        }
    }

}
