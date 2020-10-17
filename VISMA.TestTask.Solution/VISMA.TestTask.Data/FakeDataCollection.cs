using System;
using System.Collections.Generic;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public class FakeDataCollection
    {
        public static void Load(IEmployeeDbContext dbContext)
        {
            dbContext.Employee.AddRange(GetEmployeeData());
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        private static IEnumerable<Employee> GetEmployeeData()
        {
            return new List<Employee>
            {
                new Employee
                {
                    FirstName = "Bob",
                    LastName =  "Marley",
                    SocialSecurityNumber = "AS00012349",
                    CreatedOn = new DateTime(2019, 12, 4, 13, 4, 12),
                },
                new Employee
                {
                    FirstName = "Anakin",
                    LastName =  "Skywalker",
                    SocialSecurityNumber = "AS00012022",
                    PhoneNumber = "+(111)123456789",
                    CreatedOn = new DateTime(2018, 8, 21, 9, 54, 19),
                }
            };
        }
    }
}
