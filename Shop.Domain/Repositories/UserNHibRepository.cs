using NHibernate;
using Shop.Domain.Entities;
using Shop.Domain.NHibernate.Dto;

namespace Shop.Domain.Repositories
{
    public class UserNHibRepository: IUserRepository
    {
        private readonly ISession session;
        
        public UserNHibRepository(ISession session)
        {
            this.session = session;
        }

        public void CreateUser(User newUser)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save((UserDto)newUser);
                transaction.Commit();
            }
        }

        public bool UserExists(string login)
        {
            var existingUsers = session.QueryOver<UserDto>()
                                .Where(u => u.Login == login);

            return existingUsers.RowCount() > 0;
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            var dto = session.QueryOver<UserDto>()
                    .Where(u => u.Login == login
                             && u.Password == password)
                    .SingleOrDefault();

            return dto == null ? null 
                    : (User)dto;
        }
    }
}
