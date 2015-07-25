using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Shop.Domain.NHibernate
{
    public class UserMap: ClassMapping<UserModel>
    {
        public UserMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Login);
            Property(x => x.Password);
        }
    }
}