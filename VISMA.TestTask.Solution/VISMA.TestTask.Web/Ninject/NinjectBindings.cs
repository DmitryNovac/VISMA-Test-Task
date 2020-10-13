using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Data;

namespace VISMA.TestTask.Web.Ninject
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToConstant(LoggingService.GetLogger());
        }
    }
}