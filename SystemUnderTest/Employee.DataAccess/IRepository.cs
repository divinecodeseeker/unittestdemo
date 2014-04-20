using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess
{
    public interface IRepository
    {
        IList<EmployeeManagement.Data.Employee> AllEmployees { get; set; }        
    }
}
