using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.UI;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Parameters;
using Ninject.Syntax;
using Shop.Domain;
using Shop.Site.Controllers;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Shop.Site
{

    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(kernel.BeginBlock());
        }

         private void AddBindings()
        {
            //kernel.Bind(
            //    x => x.FromAssembliesMatching("Shop.Domain")
            //        .SelectAllClasses()
            //        .BindAllInterfaces()
            //);

            //kernel.Bind(
            //    x => x.FromThisAssembly()
            //        .SelectAllClasses()
            //        .BindAllInterfaces()
            //);


            kernel.Bind<IUserRepository>().To<NHibUserRepository>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserController>().To<UserController>();
        }
    }

    public class NinjectScope : IDependencyScope
    {
        protected IResolutionRoot resolutionRoot;
        public NinjectScope(IResolutionRoot kernel)
        {
            resolutionRoot = kernel;
        }
        public object GetService(Type serviceType)
        {
            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return resolutionRoot.Resolve(request).SingleOrDefault();
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return resolutionRoot.Resolve(request).ToList();
        }
        public void Dispose()
        {
            IDisposable disposable = (IDisposable)resolutionRoot;
            if (disposable != null) disposable.Dispose();
            resolutionRoot = null;
        }
    }




    //public class NinjectDependencyResolver : IDependencyResolver
    //{
    //    private readonly IKernel kernel;

    //    public NinjectDependencyResolver()
    //    {
    //        kernel = new StandardKernel();
    //        AddBindings();
    //    }
    //    public object GetService(Type serviceType)
    //    {
    //        return kernel.TryGet(serviceType);
    //    }
    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        return kernel.GetAll(serviceType);
    //    }
    //    private void AddBindings()
    //    {
    //        //kernel.Bind(
    //        //    x => x.FromAssembliesMatching("Shop.Domain")
    //        //        .SelectAllClasses()
    //        //        .BindAllInterfaces()
    //        //);

    //        //kernel.Bind(
    //        //    x => x.FromThisAssembly()
    //        //        .SelectAllClasses()
    //        //        .BindAllInterfaces()
    //        //);


    //        kernel.Bind<IUserRepository>().To<NHibUserRepository>();
    //        kernel.Bind<IUserService>().To<UserService>();
    //        kernel.Bind<IUserController>().To<UserController>();
    //    }
    //}
}