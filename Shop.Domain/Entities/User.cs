using System;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static implicit operator UserDto(User user)
        {
            return new DtoMapper<UserDto>()
                .MapFrom(user);
        }
    }
}