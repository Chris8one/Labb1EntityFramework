using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Labb1EntityFramework.Models
{
    public class LeaveApply
    {
        [Key]
        public int ApplyId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public string LeaveType { get; set; }
        [Required]
        public DateTime LeaveStartDate { get; set; }
        [Required]
        public DateTime LeaveEndDate { get; set; }
        public DateTime LeaveSubmit { get; set; }
    }
}
