using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class Client
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string MainContactEmail { get; set; }
        public string Phone { get; set; }
    }
}
