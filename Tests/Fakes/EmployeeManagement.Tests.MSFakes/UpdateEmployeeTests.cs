using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.DataAccess.Fakes;
using EmployeeManagement.Data;
using EmployeeManagement.ManagementService;

namespace EmployeeManagement.Tests.MSFakes
{
    [TestClass]
    public class UpdateEmployeeTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEmployeeTest_IdNotSame_ThrowsException()
        {
            Employee emp = new Employee();
            emp.Id = 1000;

            EmployeeManager manager = new EmployeeManager();
            manager.UpdateEmployee(1001, emp);
        }

        [TestMethod]
        public void UpdateEmployeeTest_RemoveFalseAddTrue_ReturnsFalse()
        {
            Employee emp = new Employee();
            emp.Id = 1000;

            StubIDataAccessor accessor = new StubIDataAccessor();
            bool employeeRemoved = false;
            accessor.GetEmployeeInt32 = (x) => { if (!employeeRemoved) return emp; else return null; };
            accessor.RemoveEmployeeInt32 = (x) => { employeeRemoved = true; return false; };
            accessor.AddEmployeeEmployee = (x) => {  return true; };

            EmployeeManager manager = new EmployeeManager(accessor);
            var returnValue = manager.UpdateEmployee(1000, emp);

            Assert.IsFalse(returnValue, "Returns true even on Remove False");
        }

        [TestMethod]
        public void UpdateEmployeeTest_RemoveTrueAddFalse_ReturnsFalse()
        {
            Employee emp = new Employee();
            emp.Id = 1000;

            StubIDataAccessor accessor = new StubIDataAccessor();
            bool employeeRemoved = false;
            accessor.GetEmployeeInt32 = (x) => { if (!employeeRemoved) return emp; else return null; };
            accessor.RemoveEmployeeInt32 = (x) => { employeeRemoved = true; return true; };
            accessor.AddEmployeeEmployee = (x) => { return false; };

            EmployeeManager manager = new EmployeeManager(accessor);
            var returnValue = manager.UpdateEmployee(1000, emp);

            Assert.IsFalse(returnValue, "Returns true even on Add False");
        }

        [TestMethod]
        public void UpdateEmployeeTest_RemoveTrueAddTrue_ReturnsTrue()
        {
            Employee emp = new Employee();
            emp.Id = 1000;

            StubIDataAccessor accessor = new StubIDataAccessor();
            bool employeeRemoved = false;
            accessor.GetEmployeeInt32 = (x) => { if (!employeeRemoved) return emp; else return null; };
            accessor.RemoveEmployeeInt32 = (x) => { employeeRemoved = true; return true; };
            accessor.AddEmployeeEmployee = (x) => { return true; };

            EmployeeManager manager = new EmployeeManager(accessor);
            var returnValue = manager.UpdateEmployee(1000, emp);

            Assert.IsTrue(returnValue, "Returns false even when Add and Remove are False");
        }

    }
}
