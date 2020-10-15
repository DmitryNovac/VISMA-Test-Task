using Ninject.Modules;
using VISMA.TestTask.Core.Helpers;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Core.Services;
using VISMA.TestTask.Data;

namespace VISMA.TestTask.Web.Ninject
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToConstant(LoggingService.GetLogger());
            Bind<IEmployeeDbContext>().To<DbContextService>();
            Bind<IConfigManager>().To<ConfigManager>();
            Bind<IEmployeeService>().To<EmployeeService>();
        }
    }
}