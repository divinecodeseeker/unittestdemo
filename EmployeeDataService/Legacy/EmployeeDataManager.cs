using AppliedUnitTesting.Data;
using AppliedUnitTesting.DataAccess.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedUnitTesting.DataService.Legacy
{
    public class EmployeeDataManager
    {
        EmployeeDataAccessor dataAccessor;
        List<int> availableIds;
        public virtual List<int> AvailableIds
        {
            get
            {
                if (0 == availableIds.Count)
                {
                    availableIds = GetAllEmployees().Select(x => x.Id).ToList();
                }
                return availableIds;
            }            
        }

        public EmployeeDataManager()
        {
            dataAccessor = new EmployeeDataAccessor();
            availableIds = new List<int>();
            dataAccessor.EmployeeAdded += AddEmployeeToList;
            dataAccessor.EmployeeRemoved += RemoveEmployeeFromList;
        }

        public virtual Employee GetEmployee(int employeeId)
        {
            if (0 >= employeeId)
                throw new InvalidOperationException("Employee Id cannot be less than 1.");

            return dataAccessor.GetEmployee(employeeId);
        }

        public virtual IEnumerable<Employee> GetAllEmployees()
        {
            return dataAccessor.GetEmployees(x => null != x);
        }

        public virtual bool CreateNewEmployee(Employee employeeToAdd)
        {
            if (null == employeeToAdd)
                throw new ArgumentNullException("employeeToAdd");
            if (0 >= employeeToAdd.Id)
                throw new InvalidOperationException("Employee Id cannot be less than 1.");
            if(IsEmployeeInSystem(employeeToAdd.Id))
                throw new InvalidOperationException(string.Format("Employee with Id: \"{0}\" already exists.", employeeToAdd.Id));

            return dataAccessor.AddEmployee(employeeToAdd);
        }

        public virtual bool RemoveAnEmployee(int employeeId)
        {
            if (0 >= employeeId)
                throw new InvalidOperationException("Employee Id cannot be less than 1.");
            if (!IsEmployeeInSystem(employeeId))
                throw new InvalidOperationException(string.Format("Employee with Id: \"{0}\" does not exist.", employeeId));

            return dataAccessor.RemoveEmployee(dataAccessor.GetEmployee(employeeId));
        }

        public virtual bool UpdateEmployee(Employee employee)
        {   
            if(null == employee)
                throw new ArgumentNullException("employee");

            RemoveAnEmployee(employee.Id);
            CreateNewEmployee(employee);
            return true;
        }

        private bool IsEmployeeInSystem(int employeeId)
        {
            if (AvailableIds.Contains(employeeId))
                return true;
            if (null != dataAccessor.GetEmployee(employeeId))
            {
                AvailableIds.Add(employeeId);
                return true;
            }
            return false;
        }
        private void RemoveEmployeeFromList(int employeeId)
        {
            AvailableIds.Remove(employeeId);
        }

        private void AddEmployeeToList(int employeeId)
        {
            AvailableIds.Add(employeeId);
        }
    }
}
