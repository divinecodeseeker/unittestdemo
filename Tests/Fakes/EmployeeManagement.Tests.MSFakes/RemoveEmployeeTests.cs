using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.ManagementService;
using EmployeeManagement.DataAccess.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using EmployeeManagement.Data;

namespace EmployeeManagement.Tests.MSFakes
{
    [TestClass]
    public class RemoveEmployeeTests
    {
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(InvalidOperationException))]
        public void RemoveEmployeeTest_NonExisting_ThrowsException_FakesShims()
        {
            using (ShimsContext.Create())
            {
                ShimEmployeeDataAccessor.AllInstances.GetEmployeeInt32 = (@this, empId) => { return null; };
                EmployeeManager manager = new EmployeeManager();
                manager.RemoveEmployee(100);
            }
        }

        [TestMethod]        
        public void RemoveEmployeeTest_Existing_Success_FakesShims()
        {
            using (ShimsContext.Create())
            {
                Employee emp = new Employee() { Id = 10000 };
                ShimEmployeeDataAccessor.AllInstances.GetEmployeeInt32 = (@this, empId) => { return emp; };
                ShimEmployeeDataAccessor.AllInstances.RemoveEmployeeInt32 = (@this, x) => { return true; };
                
                EmployeeManager manager = new EmployeeManager();
                var returnValue = manager.RemoveEmployee(100);

                Assert.IsTrue(returnValue, "Method returned false instead of true after deletion.");
            }
        }
    }
}
