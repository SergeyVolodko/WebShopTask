using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
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
            var path = "dsfds"; //HttpContext.Current.Server.MapPath("~/App_Data");
            //var path1 = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data";

            kernel.Bind<IUserRepository>().To<NHibUserRepository>().WithConstructorArgument("dbFolderPath", path);
            kernel.Bind<IUserService>().To<UserService>();

            //kernel.Bind(
            //    x => x.FromAssembliesMatching("Shop.Domain")
            //        .SelectAllClasses()
            //        .BindAllInterfaces()
            //);

            kernel.Bind(
               x => x.FromThisAssembly()
                   .SelectAllClasses()
                   .Where(c => c.Name.EndsWith("Controller"))
                   .BindToSelf()
           );
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