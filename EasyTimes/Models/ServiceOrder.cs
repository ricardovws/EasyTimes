using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class ServiceOrder
    {
        public int id { get; set; } 
        public string SerialCode { get; set; } //preciso criar um gerador de códigos diversos.
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string Comments { get; set; }
        public bool CheckIn { get; set; }

        //After you get the job done.
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AmountOfHours { get; set; }
        //Number of worked hours
        //*****************************

        public double TotalEarned { get; set; }
        //How much that you have been earned until this moment





        //***********************************//




    }
}
