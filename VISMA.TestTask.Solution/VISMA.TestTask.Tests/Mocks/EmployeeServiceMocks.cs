using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using VISMA.TestTask.Core.Helpers;
using VISMA.TestTask.Core.Logger;
using VISMA.TestTask.Data;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Tests.Mocks
{
    public class EmployeeServiceMocks
    {
        protected List<Employee> EmployeeData = new List<Employee>
        {
            new Employee
            {
                FirstName = "FN3",
                LastName =  "LN3",
                SocialSecurityNumber = "1001",
                PhoneNumber = "9003",
                CreatedOn = new DateTime(2003, 12, 1),
            },
            new Employee
            {
                FirstName = "FN1",
                LastName =  "LN1",
                SocialSecurityNumber = "1002",
                CreatedOn = new DateTime(2001, 12, 1),
            },
            new Employee
            {
                FirstName = "FN5",
                LastName =  "LN5",
                SocialSecurityNumber = "1003",
                PhoneNumber = "9005",
                CreatedOn = new DateTime(2005, 12, 1),
            },
            new Employee
            {
                FirstName = "FN4",
                LastName =  "LN4",
                SocialSecurityNumber = "1004",
                CreatedOn = new DateTime(2004, 12, 1),
            },
            new Employee
            {
                FirstName = "FN2",
                LastName =  "LN2",
                SocialSecurityNumber = "1005",
                PhoneNumber = "9002",
                CreatedOn = new DateTime(2002, 12, 1),
            },
        };

        protected int PageSize { get; set; }
        protected string ErrorMessage { get; set; }
        protected Exception Exception { get; set; }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        protected ILogger GetLogger()
        {
            var mock = new Mock<ILogger>();

            mock.Setup(s => s.Error(It.IsAny<object>(), It.IsAny<Exception>()))
                .Callback((object message, Exception ex) =>
                {
                    ErrorMessage = message as string;
                    Exception = ex;
                });

            return mock.Object;
        }

        protected IEmployeeDbContext GetDbContext(bool isInvalidUpdate = false, bool isInvalidSelect = false)
        {
            var mock = new Mock<IEmployeeDbContext>();

            mock.Setup(s => s.Employee)
                .Returns(() =>
                {
                    if (isInvalidSelect)
                        throw new DbEntityValidationException(); //Exception type at this point doesn't matter
                    else
                        return GetQueryableMockDbSet<Employee>(EmployeeData);
                });

            mock.Setup(s => s.SaveChanges())

                .Returns(() =>
                {
                    if (isInvalidUpdate)
                        throw new DbUnexpectedValidationException(); //Exception type at this point doesn't matter
                    else
                        return 0;
                });

            return mock.Object;
        }

        protected IConfigManager GetConfigManager()
        {
            var mock = new Mock<IConfigManager>();

            mock.Setup(s => s.EmployeePageSize)
                .Returns(() => PageSize);

            return mock.Object;
        }
    }
}
