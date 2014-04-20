using EmployeeManagement.Data;
using EmployeeManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ManagementService
{
    public class EmployeeManager
    {
        IDataAccessor dataAccessor;
        public EmployeeManager()
        {
            dataAccessor = new EmployeeDataAccessor();            
        }
        public EmployeeManager(IDataAccessor dataAccessor)
        {
            this.dataAccessor = dataAccessor;
        }

        public bool AddEmployee(Employee employee)
        {
            if (null == employee)
                throw new ArgumentNullException("employee");

            if (null != dataAccessor.GetEmployee(employee.Id))
                throw new InvalidOperationException("Employee with Same Id already exists.");

            return dataAccessor.AddEmployee(employee);
        }

        public bool RemoveEmployee(int employeeId)
        {
            var employee = dataAccessor.GetEmployee(employeeId);

            if (null == employee)
                throw new InvalidOperationException("Employee does not exist.");

            return dataAccessor.RemoveEmployee(employeeId);
        }

        public bool UpdateEmployee(int id, Employee newData)
        {
            if (id != newData.Id)
                throw new InvalidOperationException("To update an employee, Id should be same.");
            return RemoveEmployee(id) &&  AddEmployee(newData);
        }


    }
}
