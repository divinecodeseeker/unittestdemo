using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Data;

namespace EmployeeManagement.DataAccess
{
    public class EmployeeRepository : IRepository
    {
        static readonly EmployeeRepository current;
        IList<EmployeeManagement.Data.Employee> employees;
        Random random = new Random();

        private EmployeeRepository()
        {
            employees = new List<Data.Employee>()
            {
                new EmployeeManagement.Data.Employee() { FirstName="FirstNameA", LastName="LastNameA", Id=1, ManagerId=-1},
                new EmployeeManagement.Data.Employee() { FirstName="FirstNameB", LastName="LastNameB", Id=100, ManagerId=1},
                new EmployeeManagement.Data.Employee() { FirstName="FirstNameC", LastName="LastNameC", Id=200, ManagerId=1},
                new EmployeeManagement.Data.Employee() { FirstName="FirstNameD", LastName="LastNameD", Id=300, ManagerId=-200},
                new EmployeeManagement.Data.Employee() { FirstName="FirstNameE", LastName="LastNameE", Id=500, ManagerId=-300},
            };
        }
        static EmployeeRepository()
        {
            if (null == current)
            {
                current = new EmployeeRepository();
            }
        }

        public static EmployeeRepository Instance
        {
            get
            {
                return current;
            }
        }
        public IList<Data.Employee> AllEmployees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
            }
        }

    }
}
