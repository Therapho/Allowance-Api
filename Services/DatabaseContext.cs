using AllowanceFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<TaskDefinition> TaskDefinitionSet { get; set; }

        public DbSet<Role> RoleSet { get; set; }

        public DbSet<TaskGroup> TaskGroupSet { get; set; }

        public DbSet<Status> StatusSet { get; set; }
        public DbSet<TransactionCategory> TransactionCategorySet { get; set; }

        public DbSet<TaskActivity> TaskActivitySet { get; set; }

        public DbSet<TaskDay> TaskDaySet { get; set; }
        public DbSet<TaskWeek> TaskWeekSet { get; set; }
        public DbSet<TransactionLog> TransactionLogSet { get; set; }
    }
}
