using Ninject;
using VISMA.TestTask.Core.Helpers;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Core.Services;
using VISMA.TestTask.Data;

namespace VISMA.TestTask.Web.Ninject
{
    public class NinjectBindings
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {

            container.Bind<ILogger>().ToConstant(LoggingService.GetLogger());
            container.Bind<IFakeDataCollection>().To<FakeDataCollection>();
            container.Bind<IEmployeeDbContext>().To<DbContextService>();
            container.Bind<IConfigManager>().To<ConfigManager>();
            container.Bind<IEmployeeService>().To<EmployeeService>();
        }
    }
}