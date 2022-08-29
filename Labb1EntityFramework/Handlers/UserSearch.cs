using Labb1EntityFramework.Models;
using Labb1EntityFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb1EntityFramework.Handlers
{
    public class UserSearch
    {
        public static List<Employee> SearchUser(string firstName, string lastName)
        {
            try
            {
                using LeaveAppDbContext context = new LeaveAppDbContext();
                var employeeList = new List<Employee>();
                var searchUser = from employee in context.Employees
                                 where employee.FirstName.ToLower() == firstName.ToLower() && employee.LastName == lastName.ToLower()
                                 select employee;

                foreach (Employee employee in searchUser)
                {
                    employeeList.Add(employee);
                }

                return employeeList;
            }

            catch
            {
                Console.WriteLine("Something went wrong here..");
                return null;
            }
        }
    }
}
