using EasyTimes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Data
{
    public class SeedingService
    {
        private EasyTimesContext _easyTimesContext;

        public SeedingService(EasyTimesContext easyTimesContext)
        {
            _easyTimesContext = easyTimesContext;
        }

        public void Seed()
        {
            if (_easyTimesContext.Owner.Any())
            {
                return; // DB has been seeded
            }

            Owner owner = new Owner();

            owner.Name = "Unknown";
            owner.Email = "Unknown@Unknown.com";

            _easyTimesContext.Owner.Add(owner);
            _easyTimesContext.SaveChanges();
           
        }
    }
}
