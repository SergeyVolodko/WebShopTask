using System;

namespace Shop.Domain.Entities
{
    public class UserModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}