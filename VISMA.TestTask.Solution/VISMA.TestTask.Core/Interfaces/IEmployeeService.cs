using System;
using System.Data.SqlClient;
using VISMA.TestTask.Core.Data;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Core.Services
{
    public interface IEmployeeService : IDisposable
    {
        DataGridResult<Employee> GetEmployee(int pageNumber, string orderValue, SortOrder sortOrder);
        bool AddEmployee(Employee employee, out string message);
    }
}
