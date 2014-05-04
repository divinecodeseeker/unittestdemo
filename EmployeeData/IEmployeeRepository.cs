using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedUnitTesting.Data
{
    public interface IEmployeeRepository
    {
        IList<Employee> Data { get; set; }
    }
}
