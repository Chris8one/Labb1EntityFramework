using Labb1EntityFramework.Data;
using Labb1EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Handlers
{
    public class NewLeaveApplies
    {
        public static void NewLeaveApply(int employeeId, string leaveType, DateTime leaveStartDate, DateTime leaveEndDate)
        {
            try
            {
                using LeaveAppDbContext context = new LeaveAppDbContext();
                var leaveApply = new LeaveApply
                {
                    EmployeeId = employeeId,
                    LeaveType = leaveType,
                    LeaveStartDate = leaveStartDate,
                    LeaveEndDate = leaveEndDate,
                    LeaveSubmit = DateTime.Now
                };

                context.Add(leaveApply);
                context.SaveChanges();

            }

            catch
            {
                Console.WriteLine("Something went wrong here..");
            }
        }
    }
}
