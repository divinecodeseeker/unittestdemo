using EmployeeManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess
{
    public interface IDataAccessor
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees(Func<Employee, bool> condition);
        bool AddEmployee(Employee employee);
        bool RemoveEmployee(int employeeId);
        bool UpdateEmployee(int id, Employee newData);

    }
}
