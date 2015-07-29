using NHibernate;

namespace Shop.Domain
{
    public class NHibUserRepository: IUserRepository
    {
        private readonly ISession session;
        
        public NHibUserRepository(ISession session)
        {
            this.session = session;
        }

        public void CreateUser(UserModel newUser)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public bool UserExists(string login)
        {
            var existingUsers = session.QueryOver<UserModel>()
                                .Where(u => u.Login == login);

            return existingUsers.RowCount() > 0;
        }
    }
}
