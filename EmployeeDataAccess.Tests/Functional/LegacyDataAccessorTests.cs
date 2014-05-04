using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppliedUnitTesting.DataAccess.Legacy;
using AppliedUnitTesting.Data;

namespace EmployeeDataAccess.Tests.Functional
{
    [TestClass]
    public class LegacyDataAccessorTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_ShouldThrowExceptionWhenConditionIsNull()
        {
            EmployeeDataAccessor sut = new EmployeeDataAccessor();
            sut.GetEmployees(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Functional_ShouldThrowExceptionEmployeeCannotBeAdded()
        {
            EmployeeRepository.Instance.Data = null;
            EmployeeDataAccessor sut = new EmployeeDataAccessor();
            sut.AddEmployee(new Employee() { Id = 100 });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Functional_ShouldThrowExceptionEmployeeCannotBeRemoved()
        {
            EmployeeRepository.Instance.Data = null;
            EmployeeDataAccessor sut = new EmployeeDataAccessor();
            sut.RemoveEmployee(new Employee() { Id = 100 });
        }
    }
}
