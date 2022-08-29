using Labb1EntityFramework.Data;
using Labb1EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Handlers
{
    public class LeaveAppliesList
    {
        public static List<LeaveApply> Applies()
        {
            try
            {
                using LeaveAppDbContext context = new LeaveAppDbContext();
                List<LeaveApply> appliesList = new List<LeaveApply>();
                var applies = context.LeaveApplies;

                foreach (LeaveApply apply in applies)
                {
                    appliesList.Add(apply);
                }
                return appliesList;
            }

            catch
            {
                Console.WriteLine("Something went wrong here..");
                return null;
            }
        }
    }
}
