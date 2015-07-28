using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using Environment = NHibernate.Cfg.Environment;

namespace Shop.Domain.NHibernate
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;
        private static HbmMapping _mapping;
        public static string dbPath = ""; 

        public static ISession OpenSession()
        {
            //Open and return the nhibernate session
            return SessionFactory.OpenSession();
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    //Create the session factory
                    _sessionFactory = Configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    //Create the nhibernate configuration
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        public static HbmMapping Mapping
        {
            get
            {
                if (_mapping == null)
                {
                    //Create the mapping
                    _mapping = CreateMapping();
                }
                return _mapping;
            }
        }

        private static Configuration CreateConfiguration()
        {
            var configuration = new Configuration();
            //Loads properties from hibernate.cfg.xml
            
            var connectionString = String.Format("Data Source={0}\\test.db;Version=3;New=True", dbPath);

            configuration.Configure(@"E:\Projects\WebShopTask\Shop.Domain\hibernate.cfg.xml")
                .AddProperties(new Dictionary<string, string>()
                {
                    {Environment.ConnectionString, connectionString}
                });


            //Loads nhibernate mappings 
            configuration.AddDeserializedMapping(Mapping, null);

            return configuration;
        }

        private static HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            //Add the person mapping to the model mapper
            mapper.AddMappings(new List<System.Type> { typeof(UserMap) });
            //Create and return a HbmMapping of the model mapping in code
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}
