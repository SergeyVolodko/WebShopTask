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
using Shop.Domain.Factories;
using Shop.Domain.Factories.Impl;
using Shop.Domain.Repositories;
using Shop.Domain.Repositories.Impl;
using Shop.Domain.Services;
using Shop.Site.App_Start;
using Shop.Site.Controllers;

namespace Shop.Site
{
    public class AppTestingData
    {
        public string DataBasePath;
        public string ArticlesXmlPath;
    }

    public class Global : NinjectHttpApplication
    {
        private AppTestingData testingData;

        public Global()
        {   
        }

        public Global(AppTestingData testingData)
        {
            this.testingData = testingData;
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

            BindRepositories(kernel);

            kernel.Bind<ICartFactory>().To<CartFactory>();
            kernel.Bind<IOrderFactory>().To<OrderFactory>();

            BindAllServices(kernel);

            BindAllControllers(kernel);
        }

        private void BindRepositories(IKernel kernel)
        {
            var articlesXmlPath = testingData != null ? testingData.ArticlesXmlPath
                                  : HttpContext.Current.Server.MapPath("~/App_Data") + "\\articles.xml";

            kernel.Bind<IUserRepository>().To<UserNHibRepository>();
            kernel.Bind<ICartRepository>().To<CartNHibRepository>();
            kernel.Bind<IProductRepository>().To<ProductNhibRepository>();
            kernel.Bind<IOrderRepository>().To<OrderNhibRepository>();
            //kernel.Bind<IProductRepository>().To<ProductXmlRepository>()
            //    .WithConstructorArgument("xmlFile", articlesXmlPath);
        }

        private void BindAllControllers(IKernel kernel)
        {
            kernel.Bind(
                x => x.FromThisAssembly()
                    .SelectAllClasses()
                    .Where(c => c.Name.EndsWith("Controller"))
                    .BindToSelf());
        }

        private void BindAllServices(IKernel kernel)
        {
            kernel.Bind(
                x => x.FromAssemblyContaining<IUserService>()
                    .SelectAllClasses()
                    .Where(c => c.Name.EndsWith("Service"))
                    .BindAllInterfaces());
        }

        private void BindNhibernateModules(IKernel kernel)
        {
            var dataBasePath = testingData != null ? testingData.DataBasePath  
                               : HttpContext.Current.Server.MapPath("~/App_Data") + "\\WebShop.db";

            kernel.Bind<NhibernateSessionFactoryProvider>()
                .ToSelf()
                .WithConstructorArgument("dbFolder", dataBasePath);

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