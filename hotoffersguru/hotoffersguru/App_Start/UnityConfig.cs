using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using hotoffersguru.Service.APIConfiguration;
using hotoffersguru.Service;
using hotoffersguru.Areas.Portal.Controllers;

namespace hotoffersguru
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IFlipkart, Flipkart>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IController, HomeController>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}