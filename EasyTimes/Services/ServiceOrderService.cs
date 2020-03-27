using EasyTimes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Services
{
    public class ServiceOrderService
    {
        private readonly EasyTimesContext _context;

        public ServiceOrderService(EasyTimesContext context)
        {
            _context = context;
        }

        public void StartAService(ServiceOrder serviceOrder)
        {
            _context.ServiceOrder.Add(serviceOrder);
            _context.SaveChanges();
        }
    }
}
