using Ninject;
using SDK.Abstract;
using SDK.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HireRight
{
    /// <summary>
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        /// <summary>
        /// </summary>
        public NinjectDependencyResolver() { }

        /// <summary>
        /// </summary>
        /// <param name="ninjectKernel"></param>
        public NinjectDependencyResolver(IKernel ninjectKernel)
        {
            kernel = ninjectKernel;
            AddBindings(kernel);
        }

        /// <summary>
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        /// <summary>
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        /// <summary>
        /// </summary>
        /// <param name="kernel"></param>
        private static void AddBindings(IKernel kernel)
        {
            kernel.Bind(typeof(IApiSDKClient<>)).To(typeof(ApiSDKClient<>));
            kernel.Bind<IAccountsSDK>().To<AccountsSDK>();
            kernel.Bind<IClientsSDK>().To<ClientsSDK>();
            kernel.Bind<ICompaniesSDK>().To<CompaniesSDK>();
            kernel.Bind<IContactsSDK>().To<ContactsSDK>();
            kernel.Bind<ILocationsSDK>().To<LocationsSDK>();
            kernel.Bind<IOrdersSDK>().To<OrdersSDK>();
            kernel.Bind<IProductsSDK>().To<ProductsSDK>();
        }
    }
}