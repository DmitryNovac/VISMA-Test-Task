using Ninject;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Data;
using VISMA.TestTask.Web.Ninject;

namespace VISMA.TestTask.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoggingService.Configure();

            var kernel = CreateKernel();
            LoadFakeMemoryDBData(kernel);
        }

        private StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));
            RegisterServices(kernel);

            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
            var containerConfigurator = new NinjectBindings();
            containerConfigurator.Configure(kernel);
        }

        private void LoadFakeMemoryDBData(IKernel kernel)
        {
            using(var fakeData = kernel.Get<IFakeDataCollection>())
            {
                fakeData.Load();
            }
        }
    }
}
