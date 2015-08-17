using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Shop.Domain.NHibernate
{
    public class IdConvention: IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.GuidComb();
        }
    }
}
