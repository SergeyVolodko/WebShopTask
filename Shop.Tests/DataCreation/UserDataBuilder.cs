using System;
using Ploeh.AutoFixture;
using Shop.Domain.Entities;

namespace Shop.Tests.DataCreation
{
    public class UserDataBuilder
    {
        private readonly User user;

        public UserDataBuilder()
        {
            var fixture = new Fixture();
            user = fixture.Build<User>()
                .Without(u => u.Id)
                .Create();
        }

        public UserDataBuilder WithId(Guid id)
        {
            user.Id = id;
            return this;
        }

        public User Build()
        {
            return user;
        }
    }
}
