using NHibernate;
using Shop.Domain.Entities;

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
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public bool UserExists(string login)
        {
            var existingUsers = session.QueryOver<User>()
                                .Where(u => u.Login == login);

            return existingUsers.RowCount() > 0;
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            return session.QueryOver<User>()
                    .Where(u => u.Login == login
                             && u.Password == password)
                    .SingleOrDefault();
        }
    }
}
