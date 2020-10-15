using Ninject;
using System.Reflection;

namespace VISMA.TestTask.Web.Ninject
{
    public class NinjectCore
    {
        private static readonly StandardKernel Kernel;

        static NinjectCore()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
        }

        public static TInterface Get<TInterface>()
        {
            return Kernel.Get<TInterface>();
        }
    }
}