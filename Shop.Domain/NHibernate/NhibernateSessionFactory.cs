using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Shop.Domain.NHibernate
{
    public class NhibernateSessionFactory
    {
        private readonly string dataBasePath;

        public NhibernateSessionFactory(string dbPath)
        {
            dataBasePath = dbPath;
        }

        public ISessionFactory GetSessionFactory()
        {
            var connectionString = String.Format("Data Source={0}\\test.db;Version=3;New=True", dataBasePath);
            
            ISessionFactory fluentConfiguration = Fluently.Configure()
                                                    .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
                                                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserModel>())
                                                    .ExposeConfiguration(BuidSchema)
                                                    .BuildSessionFactory();
 
            return fluentConfiguration;
        }

        //WARNING THIS WILL TRY TO DROP ALL YOUR TABLES EVERYTIME YOU LAUNCH YOUR SITE. DISABLE AFTER CREATING DATABASE.
        private static void BuidSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }
    }
}
