using System;
using System.Data.SqlClient;
using System.Net;
using System.Web.Mvc;
using VISMA.TestTask.Core.Data;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Core.Services;
using VISMA.TestTask.Data.Models;
using VISMA.TestTask.Web.Data;

namespace VISMA.TestTask.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, ILogger logger)
            : base(logger)
        {
            _employeeService = employeeService;
        }

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

            var result = default(DataGridResult<EmployeeResult>);

            result = _employeeService.GetEmployee(pageNumber, orderValue, sortOrder);

            return Json(new DataHttpResponse<DataGridResult<EmployeeResult>>(result));
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

            if (!_employeeService.AddEmployee(request.ToEntity(), out var responseMessage))
                return Json(new DefaultHttpResponse(HttpStatusCode.Forbidden, responseMessage));

            return Json(new DefaultHttpResponse());
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _employeeService.Dispose();
            base.OnActionExecuted(filterContext);
        }
    }
}