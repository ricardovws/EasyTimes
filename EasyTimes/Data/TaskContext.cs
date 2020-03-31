using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyTimes.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext (DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<EasyTimes.Models.Task> Task { get; set; }
    }
}
