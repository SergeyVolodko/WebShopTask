using FluentNHibernate.Mapping;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.NHibernate.Map
{
    public class UserMap: ClassMap<UserDto>
    {
        public UserMap()
        {
            Table("User");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Login);
            Map(x => x.Password);
        }
    }
}