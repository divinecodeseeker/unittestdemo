using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess
{
    public class EmployeeDataAccessor : IDataAccessor
    {
        IRepository employeeRepository;
        public EmployeeDataAccessor()
        {
            employeeRepository = EmployeeRepository.Instance;

        }
        public EmployeeDataAccessor(IRepository repo)
        {
            employeeRepository = repo;
        }
        public Data.Employee GetEmployee(int id)
        {
            Thread.Sleep(2000);
            return employeeRepository.AllEmployees.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Data.Employee> GetEmployees(Func<Data.Employee, bool> condition)
        {
            if (null != condition)
                return employeeRepository.AllEmployees.Where(condition);
            return null;
        }

        public bool AddEmployee(Data.Employee employee)
        {
            Thread.Sleep(2000);
            bool isSuccessFul = false;
            try
            {
                employeeRepository.AllEmployees.Add(employee);
                isSuccessFul = true;
            }
            catch
            {
                
            }
            return isSuccessFul;
        }

        public bool RemoveEmployee(int employeeId)
        {
            Thread.Sleep(2000);
            bool isSuccessFul = false;
            try
            {
                employeeRepository.AllEmployees.Remove(GetEmployee(employeeId));
                isSuccessFul = true;

            }
            catch
            {

            }
            return isSuccessFul;
        }

        public bool UpdateEmployee(int id, Data.Employee newData)
        {           
            return RemoveEmployee(id) && AddEmployee(newData);
        }
    }
}
