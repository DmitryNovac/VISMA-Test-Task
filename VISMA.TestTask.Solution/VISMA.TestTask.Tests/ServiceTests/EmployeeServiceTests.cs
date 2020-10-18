using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using VISMA.TestTask.Core.Services;
using VISMA.TestTask.Data.Models;
using VISMA.TestTask.Tests.Mocks;

namespace VISMA.TestTask.Tests.ServiceTests
{
    [TestClass]
    public class EmployeeServiceTests : EmployeeServiceMocks
    {
        [TestMethod]
        public void GetEmployee_ShouldThrowArgumentException_PageNumberLessThenZero()
        {
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());

            try
            {
                service.GetEmployee(-1, default(string), default(System.Data.SqlClient.SortOrder));
                Assert.Fail("Expected ArgumentException to be thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentException), e.GetType());
                Assert.IsTrue(e.Message.Contains("cannot be negative value"));
            }           
        }

        [TestMethod]
        public void GetEmployee_ShouldThrowException_DBSelectFailed()
        {
            PageSize = 5;
            var service = new EmployeeService(GetLogger(), GetDbContext(isInvalidSelect: true), GetConfigManager());

            try
            {
                service.GetEmployee(0, default(string), default(System.Data.SqlClient.SortOrder));
                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                Assert.AreEqual("Fail to read employee data from the DB", ErrorMessage);
                Assert.IsNotNull(Exception);
            }           
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn5Person()
        {
            PageSize = 5;
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(default(int), default(string), System.Data.SqlClient.SortOrder.Unspecified);

            Assert.AreEqual(5, result.Collection.Count);
            Assert.AreEqual(1, result.TotalPageCount);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn3Person()
        {
            PageSize = 3;
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(0, default(string), System.Data.SqlClient.SortOrder.Unspecified);

            Assert.AreEqual(3, result.Collection.Count);
            Assert.AreEqual(2, result.TotalPageCount);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn2Person()
        {
            PageSize = 3;
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(1, default(string), System.Data.SqlClient.SortOrder.Unspecified);

            Assert.AreEqual(2, result.Collection.Count);
            Assert.AreEqual(2, result.TotalPageCount);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn1Person()
        {
            PageSize = 2;
            // DateTime in DB stored in UTC, but returned in local.
            var createdOn = new DateTime(2002, 12, 1).ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(2, default(string), System.Data.SqlClient.SortOrder.Ascending);

            Assert.AreEqual(1, result.Collection.Count);
            Assert.AreEqual(5, result.Collection.First().RowId);
            Assert.AreEqual("FN2", result.Collection.First().FirstName);
            Assert.AreEqual("LN2", result.Collection.First().LastName);
            Assert.AreEqual("1005", result.Collection.First().SocialSecurityNumber);
            Assert.AreEqual("9002", result.Collection.First().PhoneNumber);
            Assert.AreEqual(createdOn, result.Collection.First().CreatedOn);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn5Person_OrderByFirstNameASC()
        {
            PageSize = 5;
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(0, nameof(Employee.FirstName), System.Data.SqlClient.SortOrder.Ascending);

            Assert.AreEqual(5, result.Collection.Count);
            Assert.AreEqual(1, result.TotalPageCount);
            Assert.AreEqual("FN1", result.Collection.First().FirstName);
            Assert.AreEqual("FN5", result.Collection.Last().FirstName);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturn5Person_OrderBySCNumberNameDESC()
        {
            PageSize = 5;
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.GetEmployee(0, nameof(Employee.SocialSecurityNumber), System.Data.SqlClient.SortOrder.Descending);

            Assert.AreEqual(5, result.Collection.Count);
            Assert.AreEqual(1, result.TotalPageCount);
            Assert.AreEqual("1005", result.Collection.First().SocialSecurityNumber);
            Assert.AreEqual("1001", result.Collection.Last().SocialSecurityNumber);
        }

        [TestMethod]
        public void AddEmployee_ShouldThrowArgumentNullException()
        {
            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            
            try
            {
                service.AddEmployee(null, out var messages);
                Assert.Fail("Expected ArgumentNullException to be thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType());
            }
        }

        
        [TestMethod]
        public void AddEmployee_ShouldThrowException_DBSelectFailed()
        {
            var data = new Employee
            {
                FirstName = "FN6",
                LastName =  "LN6",
                SocialSecurityNumber = "1006",
                PhoneNumber = "9006",
            };

            try
            {
                var service = new EmployeeService(GetLogger(), GetDbContext(isInvalidSelect: true), GetConfigManager());
                service.AddEmployee(data, out var messages);

                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                Assert.AreEqual("Fail to read employee data from the DB", ErrorMessage);
                Assert.IsNotNull(Exception);
            }
        }

        [TestMethod]
        public void AddEmployee_ShouldThrowException_DBUpdateFailed()
        {
            var data = new Employee
            {
                FirstName = "FN6",
                LastName =  "LN6",
                SocialSecurityNumber = "1006",
                PhoneNumber = "9006",
            };

            try
            {
                var service = new EmployeeService(GetLogger(), GetDbContext(isInvalidUpdate: true), GetConfigManager());
                service.AddEmployee(data, out var messages);

                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                Assert.AreEqual("Fail to insert new employee to the DB", ErrorMessage);
                Assert.IsNotNull(Exception);
            }
        }

        [TestMethod]
        public void AddEmployee_ShouldReturnErrorMessage_AlreadyExists()
        {
            var data = new Employee
            {
                FirstName = "FN6",
                LastName =  "LN6",
                SocialSecurityNumber = "1005",
            };

            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.AddEmployee(data, out var messages);

            Assert.IsFalse(result);
            Assert.AreEqual(5, EmployeeData.Count);
            Assert.IsTrue(messages.Contains("1005"));
        }

        [TestMethod]
        public void AddEmployee_ShouldAddNewEmployee()
        {
            var data = new Employee
            {
                FirstName = "FN6",
                LastName =  "LN6",
                SocialSecurityNumber = "a1006",
                PhoneNumber = "9 00 6",
            };

            var service = new EmployeeService(GetLogger(), GetDbContext(), GetConfigManager());
            var result = service.AddEmployee(data, out var messages);

            Assert.IsTrue(result);
            Assert.AreEqual(6, EmployeeData.Count);
            Assert.AreEqual("Fn6", EmployeeData.Last().FirstName);
            Assert.AreEqual("Ln6", EmployeeData.Last().LastName);
            Assert.AreEqual("A1006", EmployeeData.Last().SocialSecurityNumber);
            Assert.AreEqual("9006", EmployeeData.Last().PhoneNumber);
            Assert.AreEqual(DateTime.UtcNow.Date, EmployeeData.Last().CreatedOn.Date);
            Assert.AreEqual(DateTime.UtcNow.Hour, EmployeeData.Last().CreatedOn.Hour);
        }
    }
}