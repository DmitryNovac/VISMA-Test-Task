using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Core.Services
{
    public interface IEmployeeService : IDisposable
    {
        IEnumerable<Employee> GetEmployee(int pageNumber, string orderValue, SortOrder sortOrder);
        bool AddEmployee(Employee employee, out string message);
    }
}
