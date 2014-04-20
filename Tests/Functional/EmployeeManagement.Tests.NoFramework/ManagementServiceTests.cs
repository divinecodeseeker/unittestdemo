using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.ManagementService;
using EmployeeManagement.Data;

namespace EmployeeManagement.Tests.NoFramework
{
    [TestClass]
    public class ManagementServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddEmployeeTest_NullEmployee_ThrowsException_Functional()
        {
            EmployeeManager manager = new EmployeeManager();
            manager.AddEmployee(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEmployeeTest_ExistingId_ThrowsException_Functional()
        {
            Employee emp = new Employee();
            emp.FirstName = "First Name";
            emp.LastName = "Last Name";
            emp.Id = 1;
            emp.ManagerId = -1;
            EmployeeManager manager = new EmployeeManager();
            manager.AddEmployee(emp);
        }

        [TestMethod]
        public void AddEmployeeTest_Success()
        {
            Employee emp = new Employee();
            emp.FirstName = "Test";
            emp.LastName = "User";
            emp.ManagerId = 100;
            emp.Id = 1000;

            EmployeeManager manager = new EmployeeManager();
            var success = manager.AddEmployee(emp);

            Assert.IsTrue(success, "Employee creation failed.");
        }
    }
}
