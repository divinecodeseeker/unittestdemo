﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppliedUnitTesting.DataService.Proposed;
using AppliedUnitTesting.Data;
using AppliedUnitTesting.DataAccess.Proposed;
using System.Linq;

namespace EmployeeDataAccess.Tests.Functional
{
    [TestClass]
    public class ProposedDataManagerTests
    {
        [TestMethod]
        public void Functional_Create_ShouldCreateEmployee()
        {
            //Arrange
            var repository = EmployeeRepository.Instance;
            repository.Data.Remove(new Employee() { Id = 6 });

            //Act      
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.CreateNewEmployee(new Employee() { Id = 6 });

            //Assert
            Assert.IsNotNull(sut.GetEmployee(6), "Employee not created.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_Create_ShouldThrowExceptionIfEmployeeExistOnCreation()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            Employee existingEmployee = sut.GetAllEmployees().First();

            sut.CreateNewEmployee(existingEmployee);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Functional_Create_ShouldThrowExceptionWhenEmployeeIsNull()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.CreateNewEmployee(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_Create_ShouldThrowExceptionWhenEmployeeIdLessThan1()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.CreateNewEmployee(new Employee() { Id = -1 });
        }

        [TestMethod]
        public void Functional_Remove_ShouldRemoveEmployee()
        {
            //Arrange
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            var targetId = sut.AvailableIds.Max() + 1;

            EmployeeRepository repo = EmployeeRepository.Instance;
            repo.Data.Add(new Employee() { Id = targetId });

            //Act  
            sut.RemoveAnEmployee(targetId);

            //Assert
            Assert.IsNull(sut.GetEmployee(targetId), "Employee not removed.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_Remove_ShouldThrowExceptionWhenEmployeeIdLessThan1()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.RemoveAnEmployee(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_Remove_ShouldThrowExceptionWhenEmployeeDoesNotExist()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.RemoveAnEmployee(58865);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Functional_Get_ShouldThrowExceptionWhenEmployeeIdLessThan1()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.GetEmployee(-1);
        }

        [TestMethod]
        public void Functional_Get_ShouldReturnAllEmployees()
        {
            IEmployeeRepository repo = EmployeeRepository.Instance;
            var expectedCount = repo.Data.Count;

            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            var actualCount = sut.GetAllEmployees().Count();

            Assert.AreEqual<int>(expectedCount, actualCount, "Count differs, does not returns all employees");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Functional_Update_ShouldThrowExceptionWhenEmployeeIsNull()
        {
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            sut.UpdateEmployee(null);
        }

        [TestMethod]
        public void Functional_Update_ShouldUpdateEmployee()
        {
            //Arrange
            EmployeeDataManager sut = new EmployeeDataManager(new EmployeeDataAccessor(EmployeeRepository.Instance));
            var targetId = sut.AvailableIds.Max() + 1;

            EmployeeRepository repo = EmployeeRepository.Instance;
            repo.Data.Add(new Employee() { Id = targetId });

            //Act
            sut.UpdateEmployee(new Employee() { Id = targetId, FirstName = "First", LastName = "Last" });

            //Assert
            Assert.IsTrue(sut.GetEmployee(targetId).FirstName == "First", "Employee not updated.");
        }
    }
}
