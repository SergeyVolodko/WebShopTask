using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using NHibernate;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using Shop.Domain;
using Shop.Site.App_Start;
using Shop.Site.Controllers;

namespace Shop.Site
{
    public class Global : NinjectHttpApplication
    {
        private string appDataFolderPath;

        public Global()
        {   
        }

        public Global(string appDataFolderPath)
        {
            this.appDataFolderPath = appDataFolderPath;
        }

        protected override IKernel CreateKernel()
        {
            var kernel = GetKernel();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            return kernel;
        }

        public IKernel GetKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
            BindNhibernateModules(kernel);

            kernel.Bind<IUserRepository>().To<NHibUserRepository>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind(
               x => x.FromThisAssembly()
                   .SelectAllClasses()
                   .Where(c => c.Name.EndsWith("Controller"))
                   .BindToSelf()
           );
        }

        private void BindNhibernateModules(IKernel kernel)
        {
            //var path1 = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data";

            appDataFolderPath = appDataFolderPath ?? HttpContext.Current.Server.MapPath("~/App_Data");

            kernel.Bind<NhibernateSessionFactoryProvider>()
                .ToSelf()
                .WithConstructorArgument("dbFolder", appDataFolderPath);

            kernel.Bind<ISessionFactory>()
                .ToProvider<NhibernateSessionFactoryProvider>()
                .InSingletonScope();

            kernel.Bind<ISession>()
                .ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    } 
}