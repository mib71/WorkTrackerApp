using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Models;

namespace WorkTracker.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "To Do", StatusAlias = "tod" },
                new Status { StatusId = "In Progress" , StatusAlias = "inp" },
                new Status { StatusId = "Done"  , StatusAlias = "don"}
                );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = "Low" , PriorityAlias = "low" },
                new Priority { PriorityId = "Medium" , PriorityAlias = "med" },
                new Priority { PriorityId = "High" , PriorityAlias = "hig" }
                );
        }
    }
}
