using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class Manager : Employee
    {
        public List<int> Reportees { get; set; }
    }
}
