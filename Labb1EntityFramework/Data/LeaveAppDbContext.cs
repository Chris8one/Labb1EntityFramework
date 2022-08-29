using Labb1EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labb1EntityFramework.Data
{
    public class LeaveAppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveApply> LeaveApplies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = LAPTOP-MUE17E2K\\SQLEXPRESS; Initial Catalog = LabbOneEF; Integrated Security = True;");
        }
    }
}
