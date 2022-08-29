using Labb1EntityFramework.Data;
using Labb1EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Handlers
{
    public class AddUser
    {
        public static void NewUser(Employee newUser)
        {
            try
            {
                using LeaveAppDbContext context = new LeaveAppDbContext();
                context.Employees.Add(newUser);
                context.SaveChanges();
            }

            catch
            {
                Console.WriteLine("Something went wrong here..");
            }
        }
    }
}
