using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models.ViewModels
{
    public class StartAServiceViewModel
    {
        public int id { get; set; }
        public string SerialCode { get; set; } //preciso criar um gerador de códigos diversos.
        public int ClientID { get; set; }
        public string ProjectName { get; set; }
        public string Comments { get; set; }

        //After you get the job done.
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AmountOfHours { get; set; }
        //Number of worked hours
        //*****************************
        List<Charger> Chargers = new List<Charger>();




        //***********************************//

    }
}
