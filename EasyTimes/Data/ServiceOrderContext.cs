using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyTimes.Models
{
    public class ServiceOrderContext : DbContext
    {
        public ServiceOrderContext (DbContextOptions<ServiceOrderContext> options)
            : base(options)
        {
        }

        public DbSet<EasyTimes.Models.ServiceOrder> ServiceOrder { get; set; }
    }
}
