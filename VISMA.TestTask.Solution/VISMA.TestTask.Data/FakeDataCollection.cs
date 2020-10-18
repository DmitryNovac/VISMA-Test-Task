using System;
using System.Collections.Generic;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public class FakeDataCollection : IFakeDataCollection
    {
        private IEmployeeDbContext _dbContext;

        public FakeDataCollection(IEmployeeDbContext employeeDbContext)
        {
            _dbContext = employeeDbContext;
        }

        public void Load()
        {
            _dbContext.Employee.AddRange(GetEmployeeData());
            _dbContext.SaveChanges();
            _dbContext.Dispose();
        }

        private IEnumerable<Employee> GetEmployeeData()
        {
            return new List<Employee>
            {
                new Employee
                {
                    FirstName = "Bob",
                    LastName =  "Kenobi",
                    SocialSecurityNumber = "AS00012349",
                    CreatedOn = new DateTime(2019, 12, 4, 13, 4, 12),
                },
                new Employee
                {
                    FirstName = "Luke",
                    LastName =  "Skywalker",
                    SocialSecurityNumber = "AS00012022",
                    PhoneNumber = "+(111)123456789",
                    CreatedOn = new DateTime(2018, 8, 21, 9, 54, 19),
                }
            };
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
