using FluentNHibernate.Mapping;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Map
{
    public class UserMap: ClassMap<UserModel>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Login);
            Map(x => x.Password);
        }
    }
}