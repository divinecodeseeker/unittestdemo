using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using AppliedUnitTesting.Data;
using AppliedUnitTesting.DataAccess.Proposed;
using System.Collections.Generic;

namespace EmployeeDataAccess.Tests.Moq
{
    [TestClass]
    public class ProposedDataAccessorTests
    {
        Mock<IEmployeeRepository> employeeRepo;
        IList<Employee> myList = new List<Employee>()
        {
            new Employee() { FirstName = "FN1", Id = 1, LastName = "LN1" },
            new Employee() { FirstName = "FN2", Id = 2, LastName = "LN2" },
            new Employee() { FirstName = "FN3", Id = 3, LastName = "LN3" },
            new Employee() { FirstName = "FN4", Id = 4, LastName = "LN4" },
            new Employee() { FirstName = "FN5", Id = 5, LastName = "LN5" }
        };

        [TestInitialize]
        public void InstantiateMocksForTest()
        {
            employeeRepo = new Mock<IEmployeeRepository>();
        }

        [TestMethod] 
        public void Moq_GetEmployee_ShouldCallDataGetter()
        {
            //Arrange
            employeeRepo = new Mock<IEmployeeRepository>();
            employeeRepo.SetupGet(x => x.Data).Returns(myList);

            //Act
            EmployeeDataAccessor sut = new EmployeeDataAccessor(employeeRepo.Object);
            var employee = sut.GetEmployee(2);

            //Assert
            employeeRepo.VerifyGet(x => x.Data);
            Assert.IsNotNull(employee, "Null returned.");
        }

        [TestMethod]
        public void Moq_GetEmployee_ShouldCallGetEmployees()
        {
            //Arrange
            employeeRepo = new Mock<IEmployeeRepository>();
            Mock<EmployeeDataAccessor> sut = new Mock<EmployeeDataAccessor>(employeeRepo.Object);
            sut.CallBase = true;
            sut.Setup(x => x.GetEmployees(It.IsAny<Func<Employee, bool>>()))
               .Returns(
                         new List<Employee>()
                            {
                                new Employee() { Id = 10 }
                            }
                        );

            //Act           
            var employee = sut.Object.GetEmployee(2);

            //Assert
            sut.Verify(x => x.GetEmployees(It.IsAny<Func<Employee, bool>>()));
            Assert.IsNotNull(employee, "Null returned.");
            Assert.IsTrue(employee.Id == 10, "Does not returns employee from GetEmployees.");
        }

        [TestMethod]
        public void Moq_GetEmployee_ShouldReturnNullIfEmployeeNotFound()
        {
            //Arrange
            employeeRepo = new Mock<IEmployeeRepository>();
            employeeRepo.SetupGet(x => x.Data).Returns(myList);

            //Act
            EmployeeDataAccessor sut = new EmployeeDataAccessor(employeeRepo.Object);
            var employee = sut.GetEmployee(10);

            //Assert
            Assert.IsNull(employee, "Employee is not null.");
        }


    }
}
