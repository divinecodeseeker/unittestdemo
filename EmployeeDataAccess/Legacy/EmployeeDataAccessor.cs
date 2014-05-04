using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppliedUnitTesting.Data;

namespace AppliedUnitTesting.DataAccess.Legacy
{
    public class EmployeeDataAccessor
    {
        readonly IEmployeeRepository repository;
        public event Action<int> EmployeeAdded;
        public event Action<int> EmployeeRemoved;
        public EmployeeDataAccessor()
        {
            repository = EmployeeRepository.Instance;
        }

        public Employee GetEmployee(int id)
        {
           return GetEmployees(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees(Func<Employee, bool> condition)
        {
            if (null == condition)
                throw new InvalidOperationException("Invalid condition provided.");

            return repository.Data.Where(condition);
        }

        public bool AddEmployee(Employee item)
        {
            bool returnValue = false;
            try
            {
                repository.Data.Add(item);
                returnValue = true;
                if (null != EmployeeAdded)
                {
                    EmployeeAdded(item.Id);
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return returnValue;
        }

        public bool RemoveEmployee(Employee employee)
        {
            bool returnValue = false;
            try
            {
                repository.Data.Remove(employee);
                returnValue = true;
                if (null != EmployeeRemoved)
                {
                    EmployeeRemoved(employee.Id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnValue;
        }
    }
}
