using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyTimes.Models
{
    public class ChargerContext : DbContext
    {
        public ChargerContext (DbContextOptions<ChargerContext> options)
            : base(options)
        {
        }

        public DbSet<EasyTimes.Models.Charger> Charger { get; set; }
    }
}
