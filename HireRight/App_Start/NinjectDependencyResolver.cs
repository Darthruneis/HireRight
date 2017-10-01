using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Concrete;
using HireRight.Repository.Abstract;
using HireRight.Repository.Concrete;
using Ninject.Activation;

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
            //SDK Bindings
            //kernel.Bind<IApiSDKClient>().To<ApiSDKClient>();
            //kernel.Bind<ICompaniesSDK>().To<CompaniesSDK>();
            //kernel.Bind<IContactsSDK>().To<ContactsSDK>();
            //kernel.Bind<IOrdersSDK>().To<OrdersSDK>();
            //kernel.Bind<IProductsSDK>().To<ProductsSDK>();
            //kernel.Bind<ICategoriesSDK>().To<CategoriesSDK>();

            //repository bindings
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            kernel.Bind<IContactsRepository>().To<ContactsRepository>();
            kernel.Bind<IOrdersRepository>().To<OrdersRepository>();
            kernel.Bind<IProductsRepository>().To<ProductsRepository>();
            kernel.Bind<ICategoriesRepository>().To<CategoriesRepository>();

            //business logic bindings
            kernel.Bind<ICompanyBusinessLogic>().To<CompanyBusinessLogic>();
            kernel.Bind<IContactsBusinessLogic>().To<ContactsBusinessLogic>();
            kernel.Bind<IOrdersBusinessLogic>().To<OrdersBusinessLogic>();
            kernel.Bind<IProductsBusinessLogic>().To<ProductsBusinessLogic>();
            kernel.Bind<ICategoriesBusinessLogic>().To<CategoriesBusinessLogic>();

            Func<IContext, object> getEmailTemplatePath = context =>
                            {
                                string baseDir = System.Web.HttpContext.Current.Server.MapPath("~");
                                return Path.GetFullPath(baseDir + @"\EmailBase.cshtml");
                            };
            kernel.Bind<IEmailSender>().To<EmailSender>().WithConstructorArgument("emailTemplatePath", getEmailTemplatePath);
        }
    }
}