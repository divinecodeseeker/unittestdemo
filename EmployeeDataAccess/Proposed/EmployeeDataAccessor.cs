using AppliedUnitTesting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedUnitTesting.DataAccess.Proposed
{
    public class EmployeeDataAccessor : IEmployeeDataAccessor
    {
        readonly IEmployeeRepository repository;
        public EmployeeDataAccessor(IEmployeeRepository employeeRepository)
        {
            repository = employeeRepository;
        }

        public virtual Employee GetEmployee(int id)
        {
            return GetEmployees(x => x.Id == id).FirstOrDefault();
        }

        public virtual IEnumerable<Employee> GetEmployees(Func<Employee, bool> condition)
        {
            if (null == condition)
                throw new InvalidOperationException("Invalid condition provided.");

            return repository.Data.Where(condition);
        }

        public virtual bool AddEmployee(Employee item)
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

        public virtual bool RemoveEmployee(Employee employee)
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
               

        public event Action<int> EmployeeAdded;

        public event Action<int> EmployeeRemoved;
    }
}
