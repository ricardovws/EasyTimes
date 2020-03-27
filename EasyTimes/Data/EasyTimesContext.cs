using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;

namespace EasyTimes.Models
{
    public class EasyTimesContext : DbContext
    {
        public EasyTimesContext (DbContextOptions<EasyTimesContext> options)
            : base(options)
        {
        }

        public DbSet<EasyTimes.Models.Owner> Owner { get; set; }

        public DbSet<EasyTimes.Models.Charger> Charger { get; set; }

        public DbSet<EasyTimes.Models.Client> Client { get; set; }

        public DbSet<EasyTimes.Models.LittleTask> LittleTask { get; set; }

        public DbSet<EasyTimes.Models.ServiceOrder> ServiceOrder { get; set; }
    }
}
