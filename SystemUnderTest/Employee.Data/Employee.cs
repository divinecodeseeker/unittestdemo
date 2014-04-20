using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class Employee : Person
    {
        public int Id { get; set; }        
        public int ManagerId { get; set; }
    }
}
