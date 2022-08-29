using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<LeaveApply> LeaveApplies { get; set; }
    }
}