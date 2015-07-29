using NHibernate;
using Ninject.Activation;
using Shop.Domain.NHibernate;

namespace Shop.Site
{
    public class NhibernateSessionFactoryProvider : Provider<ISessionFactory>
    {
        private readonly string dataBasePath;

        public NhibernateSessionFactoryProvider(string dbFolder)
        {
            dataBasePath = dbFolder;
        }

        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new NhibernateSessionFactory(dataBasePath);
            return sessionFactory.GetSessionFactory();
        }
    }
}
