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
                new Status { StatusId = "tod", StatusName = "To Do" },
                new Status { StatusId = "inp", StatusName = "In Progress" },
                new Status { StatusId = "don", StatusName = "Done" }
                );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = "low", PriorityName = "Low" },
                new Priority { PriorityId = "med", PriorityName = "Medium" },
                new Priority { PriorityId = "hig", PriorityName = "High" }
                );
        }
    }
}
