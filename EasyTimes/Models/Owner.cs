using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class Owner
    {
        //Personal data:
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //Banking account data:
        public string Bank { get; set; }
        public int Agency { get; set; }
        public string CurrentAccount { get; set; }
        //Prices and Costs
        public double PricePerHour { get; set; } //How much you earn per hour
        public double GasPrice { get; set; } //Price paid for the gasoline
        public double OvertimeProfitRate { get; set; } //Percentual that will increase you salary. 
    }
}
