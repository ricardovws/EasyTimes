using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models.ViewModels
{
    public class EditProfileViewModel
    {
        //Personal data:
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //Banking account data:
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string CurrentAccount { get; set; }
        //Prices and Costs
        public string PricePerHour { get; set; } //How much you earn per hour
        public string GasPrice { get; set; } //Price paid for the gasoline
        public string NormalTime { get; set; } //normal workload. Everything more than that is overtime.
        public string TimeToMealTicket { get; set; } // Time to receive meal ticket.
        public string MealTicket { get; set; } //Value of your meal ticket
        public string OvertimeProfitRate { get; set; } //Percentual that will increase you salary. 
    }
}
