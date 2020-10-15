using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VISMA.TestTask.Core.Services;
using VISMA.TestTask.Web.Data;
using VISMA.TestTask.Web.Ninject;

namespace VISMA.TestTask.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        /// <summary>
        /// Gets list view
        /// </summary>
        /// <returns>List view</returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// Gets employee list by page
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="orderValue">Employee column order value (default: Id)</param>
        /// <param name="sortOrder">Sort order (default: ASC = 1; DESC = 0)</param>
        /// <returns>DataHttpResponse with List of employee</returns>
        [HttpPost]
        public ActionResult GetEmployeeList(int pageNumber, string orderValue, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (pageNumber < 0)
                throw new ArgumentException($"Parameter '{nameof(pageNumber)}' cannot be negative value!");

            var result = default(List<object>);

            using (var service = NinjectCore.Get<IEmployeeService>())
            {
                result = service.GetEmployee(pageNumber, orderValue, sortOrder).Cast<object>().ToList();
            }

            return Json(new DataHttpResponse<List<object>>(result));
        }

        /// <summary>
        /// Add new employee to DB
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>DefaultHttpResponse</returns>
        [HttpPost]
        public ActionResult AddNewEmployer(EmployeeModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Can be also implemented as method attribute
            if (!ValidateRequest(out var errorMessages))
                return Json(new DefaultHttpResponse(HttpStatusCode.Forbidden, string.Join("\n", errorMessages)));

            using (var service = NinjectCore.Get<IEmployeeService>())
            {
                if (!service.AddEmployee(request.ToEntity(), out var responseMessage))
                    return Json(new DefaultHttpResponse(HttpStatusCode.Forbidden, responseMessage));
            }

            return Json(new DefaultHttpResponse());
        }
    }
}