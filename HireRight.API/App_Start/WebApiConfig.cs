using HireRight.API.Handlers;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace HireRight.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.MessageHandlers.Add(new ApiResponseWrappingHandler());
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi",
                                       "api/{controller}/{id}",
                                       defaults: new { id = RouteParameter.Optional }
                );
        }

        ////business logic bindings
        //kernel.Bind(typeof(IBusinessLogicBase<,>)).To(typeof(BusinessLogicBase<,>));
        //kernel.Bind<IAccountsBusinessLogic>().To<AccountsBusinessLogic>();
        //kernel.Bind<IClientsBusinessLogic>().To<ClientsBusinessLogic>();
        //kernel.Bind<ICompanyBusinessLogic>().To<CompanyBusinessLogic>();
        //kernel.Bind<IContactsBusinessLogic>().To<ContactsBusinessLogic>();
        //kernel.Bind<ILocationsBusinessLogic>().To<LocationsBusinessLogic>();
        //kernel.Bind<IOrdersBusinessLogic>().To<OrdersBusinessLogic>();
        //kernel.Bind<IProductsBusinessLogic>().To<ProductsBusinessLogic>();

        ////repository bindings
        //kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
        //kernel.Bind<IAccountsRepository>().To<AccountsRepository>();
        //kernel.Bind<IClientsRepository>().To<ClientsRepository>();
        //kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
        //kernel.Bind<IContactsRepository>().To<ContactsRepository>();
        //kernel.Bind<ILocationsRepository>().To<LocationsRepository>();
        //kernel.Bind<IOrdersRepository>().To<OrdersRepository>();
        //kernel.Bind<IProductsRepository>().To<ProductsRepository>();
    }
}