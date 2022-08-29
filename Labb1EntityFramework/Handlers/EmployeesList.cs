using Labb1EntityFramework.Data;
using Labb1EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Handlers
{
    public class EmployeesList
    {
        public static List<Employee> Employees()
        {
            try
            {
                using LeaveAppDbContext context = new LeaveAppDbContext();
                var employeeList = new List<Employee>();
                var employees = context.Employees;

                foreach (Employee employee in employees)
                {
                    employeeList.Add(employee);
                }
                return employeeList;
            }

            catch
            {
                Console.WriteLine("Something went wrong..");
                return null;
            }
        }
    }
}
