using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Concrete;
using HireRight.Repository.Abstract;
using HireRight.Repository.Concrete;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HireRight.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HireRight.API.App_Start.NinjectWebCommon), "Stop")]

namespace HireRight.API.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new OwinNinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //repository bindings
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IAccountsRepository>().To<AccountsRepository>();
            kernel.Bind<IClientsRepository>().To<ClientsRepository>();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            kernel.Bind<IContactsRepository>().To<ContactsRepository>();
            kernel.Bind<ILocationsRepository>().To<LocationsRepository>();
            kernel.Bind<IOrdersRepository>().To<OrdersRepository>();
            kernel.Bind<IProductsRepository>().To<ProductsRepository>();

            //business logic bindings
            kernel.Bind<IAccountsBusinessLogic>().To<AccountsBusinessLogic>();
            kernel.Bind<IClientsBusinessLogic>().To<ClientsBusinessLogic>();
            kernel.Bind<ICompanyBusinessLogic>().To<CompanyBusinessLogic>();
            kernel.Bind<IContactsBusinessLogic>().To<ContactsBusinessLogic>();
            kernel.Bind<ILocationsBusinessLogic>().To<LocationsBusinessLogic>();
            kernel.Bind<IOrdersBusinessLogic>().To<OrdersBusinessLogic>();
            kernel.Bind<IProductsBusinessLogic>().To<ProductsBusinessLogic>();
        }
    }
}