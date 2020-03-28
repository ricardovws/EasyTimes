using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class LittleTask
    {
        public int id { get; set; }

        public int ServiceOrderID { get; set; }

        //public string Json { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double BetweenBoth { get; set; } // End - Start
    }
}
