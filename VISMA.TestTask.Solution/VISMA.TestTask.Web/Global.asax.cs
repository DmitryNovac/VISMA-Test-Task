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
            FakeDataCollection.Load(NinjectCore.Get<IEmployeeDbContext>());
        }
    }
}
