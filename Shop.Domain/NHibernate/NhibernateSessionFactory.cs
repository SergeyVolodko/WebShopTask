using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Shop.Domain.Entities;

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
            var connectionString = String.Format("Data Source={0};Version=3;New=True", dataBasePath);

            var fluentConfiguration = Fluently.Configure()
                                               .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
                                               .Mappings(m => m.AutoMappings.Add(
                                                   AutoMap.AssemblyOf<User>()
                                                   .Where(a => a.Namespace.EndsWith("Entities") && a.Name != "NotAuthorizedUser")
                                                   .Conventions.Add<IdConvention>()
                                                   .Conventions.Add(DefaultCascade.All()))
                                                   )
                                               .ExposeConfiguration(BuidSchema)
                                               .BuildConfiguration();
 
            return fluentConfiguration.BuildSessionFactory();
        }

        //WARNING THIS WILL TRY TO DROP ALL YOUR TABLES EVERYTIME YOU LAUNCH YOUR SITE. DISABLE AFTER CREATING DATABASE.
        private static void BuidSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }
    }
}
