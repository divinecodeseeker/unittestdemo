using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedUnitTesting.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {

        IList<Employee> employeeList;
        static EmployeeRepository current;

        private EmployeeRepository()
        {
            employeeList = new List<Employee>();
            employeeList.Add(new Employee() { FirstName = "FN1", Id = 1, LastName = "LN1" });
            employeeList.Add(new Employee() { FirstName = "FN2", Id = 2, LastName = "LN2" });
            employeeList.Add(new Employee() { FirstName = "FN3", Id = 3, LastName = "LN3" });
            employeeList.Add(new Employee() { FirstName = "FN4", Id = 4, LastName = "LN4" });
            employeeList.Add(new Employee() { FirstName = "FN5", Id = 5, LastName = "LN5" });            
        }
        static EmployeeRepository()
        {
            if (null == current)
            {
                current = new EmployeeRepository();
            }
        }

        public IList<Employee> Data
        {
            get
            {
                System.Threading.Thread.Sleep(1000);
                return employeeList;
            }
            set
            {
                employeeList = value;
            }
        }

        public static EmployeeRepository Instance
        {
            get
            {
                return current;
            }
        }
    }
}
