using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;
using VISMA.TestTask.Core.Data;
using VISMA.TestTask.Core.Extensions;
using VISMA.TestTask.Core.Helpers;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Core.Properties;
using VISMA.TestTask.Data;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger _logger;
        private readonly IEmployeeDbContext _employeeDbContext;
        private readonly IConfigManager _configManager;

        public EmployeeService(ILogger logger, IEmployeeDbContext employeeDbContext, IConfigManager configManager)
        {
            _logger = logger;
            _employeeDbContext = employeeDbContext;
            _configManager = configManager;
        }

        public DataGridResult<Employee> GetEmployee(int pageNumber, string orderValue, SortOrder sortOrder)
        {
            if (pageNumber < 0)
                throw new ArgumentException($"Parameter '{nameof(pageNumber)}' cannot be negative value!");

            if (string.IsNullOrWhiteSpace(orderValue))
                orderValue = nameof(Employee.Id);

            if (sortOrder == SortOrder.Unspecified)
                sortOrder = SortOrder.Ascending;

            var result = default(DataGridResult<Employee>);

            try
            {
                result = new DataGridResult<Employee>(
                    data: _employeeDbContext.Employee
                        .OrderBy($"{orderValue} {sortOrder.ToString()}")
                        .Skip(_configManager.EmployeePageSize * pageNumber)
                        .Take(_configManager.EmployeePageSize)
                        .ToList(),
                    totalRowCount: _employeeDbContext.Employee.Count(),
                    pageNumber: pageNumber,
                    pageSize: _configManager.EmployeePageSize,
                    wrapper: EmployeeDataWrapper);
            }
            catch (Exception e)
            {
                _logger.Error("Fail to read employee data from the DB", e);
                throw;
            }

            return result;
        }

        private object EmployeeDataWrapper(Employee item, int rowNumber)
        {
            return new
            {
                RowId = rowNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                SocialSecurityNumber = item.SocialSecurityNumber,
                PhoneNumber = item.PhoneNumber,
                CreatedOn = item.CreatedOn.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
            };
        }

        public bool AddEmployee(Employee employee, out string message)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            message = null;

            employee.SocialSecurityNumber = employee.SocialSecurityNumber.ToUpper();
            
            try
            {
               var existingperson =  _employeeDbContext.Employee
                    .FirstOrDefault(o => o.SocialSecurityNumber == employee.SocialSecurityNumber);

               if (existingperson != null)
               {
                   message = string.Format(ServerResponseMessages.PersonAlreadyExists, employee.SocialSecurityNumber);
                   return false;
               }
            }
            catch (Exception e)
            {
                _logger.Error("Fail to read employee data from the DB", e);
                throw;
            }

            employee.CreatedOn = DateTime.UtcNow;
            employee.PhoneNumber = employee.PhoneNumber?.Replace(" ", string.Empty);
            employee.LastName = employee.LastName.ToCapitalize();
            employee.FirstName = employee.FirstName.ToCapitalize();
            

            try
            {
                _employeeDbContext.Employee.Add(employee);
                _employeeDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.Error("Fail to insert new employee to the DB", e);
                throw;
            }

            return true;
        }

        public void Dispose()
        {
            _employeeDbContext.Dispose();
        }
    }
}
