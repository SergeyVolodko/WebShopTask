using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

namespace Shop.Site.App_Start
{
    public static class NinjectWebCommon
    {
        //static NinjectWebCommon()
        //{
        //    Kernel = new InterceptAllModule();
        //}

        //private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        //public static void Start()
        //{
        //    DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
        //    DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        //    bootstrapper.Initialize(() => CreateKernel(Kernel));
        //}

        //public static void Stop()
        //{
        //    bootstrapper.ShutDown();
        //}

        //private static IKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    try
        //    {
        //        kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
        //        kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

        //        RegisterServices(kernel);
        //        return kernel;
        //    }
        //    catch
        //    {
        //        kernel.Dispose();
        //        throw;
        //    }
        //}

        //private static void RegisterServices(IKernel kernel)
        //{
        //    kernel.Bind<MyInterface>().To<MyClass>().InSingletonScope();
        //}
    }
}