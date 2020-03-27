using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class ServiceOrder
    {
        public int id { get; set; }
        public string SerialCode { get; set; }
        public int ClientID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public double AmountOfHours { get; set; } //Number of worked hours
       


        //***********************************//

       


    }
}
