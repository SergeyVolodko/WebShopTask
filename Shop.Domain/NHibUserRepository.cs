using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Shop.Domain.NHibernate;

namespace Shop.Domain
{
    public class NHibUserRepository: IUserRepository
    {
        public NHibUserRepository(string dbFolderPath)
        {
            NHibernateHelper.dbPath = dbFolderPath;
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(Console.WriteLine, true);
        }

        public void CreateUser(UserModel newUser)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(newUser);
                transaction.Commit();
            }
        }

        public bool UserExists(string login)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var existingUsers = session.QueryOver<UserModel>()
                                    .Where(p => p.Login == login);

                return existingUsers.RowCount() > 0;
            }
        }
    }
}
