using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppliedUnitTesting.DataService.Legacy;
using AppliedUnitTesting.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using AppliedUnitTesting.Data.Fakes;
using AppliedUnitTesting.DataAccess.Legacy.Fakes;

namespace EmployeeDataAccess.Tests.MSFakes.UsingShims
{
    [TestClass]
    public class LegacyDataManagerTests
    {
        IList<Employee> myList = new List<Employee>()
        {
            new Employee() { FirstName = "FN1", Id = 1, LastName = "LN1" },
            new Employee() { FirstName = "FN2", Id = 2, LastName = "LN2" },
            new Employee() { FirstName = "FN3", Id = 3, LastName = "LN3" },
            new Employee() { FirstName = "FN4", Id = 4, LastName = "LN4" },
            new Employee() { FirstName = "FN5", Id = 5, LastName = "LN5" }
        };

        [TestMethod]
        public void Shims_Create_ShouldCreateEmployee()
        {
            using (ShimsContext.Create())
            {
                //Arrange
                bool wasAddEmployeeCalled = false;
                ShimEmployeeDataAccessor.AllInstances.AddEmployeeEmployee = (@this, employee) => { wasAddEmployeeCalled = true; return true; };
                ShimEmployeeDataAccessor.AllInstances.GetEmployeesFuncOfEmployeeBoolean = (@this, x) => { return myList; };
                ShimEmployeeDataAccessor.AllInstances.GetEmployeeInt32 = (@this, x) => { return null; };

                //Act      
                EmployeeDataManager sut = new EmployeeDataManager();
                sut.CreateNewEmployee(new Employee() { Id = 6 });

                //Assert
                Assert.IsTrue(wasAddEmployeeCalled, "Add Employee was not called.");
            }
            
        }
       
    }
}
