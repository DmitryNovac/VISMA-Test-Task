using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Web.Ninject;

namespace VISMA.TestTask.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;

        public BaseController()
        {
            _logger = NinjectCore.Get<ILogger>();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            _logger.Error(Server.GetLastError());
            

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/InternalError.cshtml"
            };
        }

        protected bool ValidateRequest(out List<string> errorMessages)
        {
            errorMessages = null;

            if (ModelState.IsValid)
                return true;

            errorMessages = new List<string>();

            foreach (var key in ModelState.Keys)
            {
                errorMessages.AddRange(ModelState[key].Errors
                    .Select(o => o.ErrorMessage)
                    .ToList());
            }

            return false;
        }
    }
}