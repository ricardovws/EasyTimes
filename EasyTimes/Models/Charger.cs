using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTimes.Models
{
    public class Charger
    {
        public DateTime Start_ { get; set; } //Moment that you get started with a task
        public DateTime End_ { get; set; } //Moment that you stop working with a task

        public double Hours_ { get; set; } //Number of hours between Start_ and End_

        public double OnTheRanch { get; set; } //Distance in km that you ran through
    }
}
