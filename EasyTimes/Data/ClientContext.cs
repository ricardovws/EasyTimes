using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyTimes.Models
{
    public class ClientContext : DbContext
    {
        public ClientContext (DbContextOptions<ClientContext> options)
            : base(options)
        {
        }

        public DbSet<EasyTimes.Models.Client> Client { get; set; }
    }
}
