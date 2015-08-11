using System;
using Shop.Domain.Entities;

namespace Shop.Domain.NHibernate.Dto
{
    public class UserDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public static implicit operator User(UserDto dto)
        {
            return new DtoMapper<User>().MapFrom(dto);
        }
    }
}