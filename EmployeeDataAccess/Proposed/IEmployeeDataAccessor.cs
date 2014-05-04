using System;
namespace AppliedUnitTesting.DataAccess.Proposed
{
    public interface IEmployeeDataAccessor
    {
        bool AddEmployee(AppliedUnitTesting.Data.Employee item);
        AppliedUnitTesting.Data.Employee GetEmployee(int id);
        System.Collections.Generic.IEnumerable<AppliedUnitTesting.Data.Employee> GetEmployees(Func<AppliedUnitTesting.Data.Employee, bool> condition);
        bool RemoveEmployee(AppliedUnitTesting.Data.Employee employee);

        event Action<int> EmployeeAdded;
        event Action<int> EmployeeRemoved;
    }
}
