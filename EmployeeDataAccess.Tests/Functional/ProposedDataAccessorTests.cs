using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppliedUnitTesting.DataAccess.Proposed;
using AppliedUnitTesting.Data;

namespace EmployeeDataAccess.Tests.Functional
{
    [TestClass]
    public class ProposedDataAccessorTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_ShouldThrowExceptionWhenConditionIsNull()
        {
            EmployeeDataAccessor sut = new EmployeeDataAccessor(EmployeeRepository.Instance);
            sut.GetEmployees(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes=true)]
        public void Functional_ShouldThrowExceptionEmployeeCannotBeAdded()
        {
            EmployeeDataAccessor sut = new EmployeeDataAccessor(null);
            sut.AddEmployee(new Employee() { Id = 100 });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Functional_ShouldThrowExceptionEmployeeCannotBeRemoved()
        {
            EmployeeDataAccessor sut = new EmployeeDataAccessor(null);
            sut.RemoveEmployee(new Employee() { Id = 100 });
        }
    }
}
