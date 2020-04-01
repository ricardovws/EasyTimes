using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models.ViewModels
{
    public class ReportToPrintViewModel
    {
       
        public List<LittleTask> _list { get; set; }
   
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string Comments { get; set; }


      
        public double AmountOfHours { get; set; } //horas totais do projeto

        public double Overtime { get; set; } //hora-extra.
        public double MealTicketValue { get; set; } // VR

        public double OnTheRach { get; set; } //kilometragem
                                              //Number of worked hours
                                              //*****************************


        public double TotalEarned { get; set; }
        //How much that you have been earned until this moment





        //***********************************//
    }
}
