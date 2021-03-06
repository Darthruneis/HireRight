﻿using Ninject;
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
            kernel.Bind<IApiSDKClient>().To<ApiSDKClient>();
            kernel.Bind<ICompaniesSDK>().To<CompaniesSDK>();
            kernel.Bind<IContactsSDK>().To<ContactsSDK>();
            kernel.Bind<IOrdersSDK>().To<OrdersSDK>();
            kernel.Bind<IProductsSDK>().To<ProductsSDK>();
            kernel.Bind<ICategoriesSDK>().To<CategoriesSDK>();
        }
    }
}