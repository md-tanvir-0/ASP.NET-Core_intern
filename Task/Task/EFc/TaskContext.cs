using Microsoft.EntityFrameworkCore;
using System;
using Task.EFc.Tables;

namespace Task.EFc
{
    public class TaskContext:DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
       : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
    }
}
