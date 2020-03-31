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

        public void TotalValues(int id)
        {
            //Esse método vai atualizar tudo na service order.
            //pegar owner
            var owner = _context.Owner.FirstOrDefault();
            //pegar order
            var order = _context.ServiceOrder.Where(x => x.id == id).First();
            //gera list com todas littletask
            var list = _context.LittleTask.Where(x => x.ServiceOrderID == id).ToList();

            //criar listas usando como key a data. Ou seja, cada data vai ter uma lista com as littletasks referentes a ela.
            IEnumerable<IGrouping<DateTime, int>> query =
               list.GroupBy(x => x.Date, x => x.id);

            //vai criar uma nova lista de littetasks.

            var listOfLittleTasks = new List<LittleTask>();

            //vai criar uma littletask nova que irá receber como date a key de uma das listas criadas, e depois disso
            //em between hours vai ir a soma de todas as horas da lista da key citada.
            //isso será feito para todas as listas de key.

            foreach (IGrouping<DateTime, int> littleTasks in query)
            {
                double __workload = 0.00;
                LittleTask __littleTask = null; // puxada do banco
                LittleTask __littleTask__ = new LittleTask(); //nova, que recebe key como date e sum total como between hours.
                foreach (int ID in littleTasks)
                {

                    __littleTask = list.FirstOrDefault(x => x.id == ID);
                    __workload += __littleTask.BetweenBoth;
                    __littleTask__.BetweenBoth = __workload;
                    __littleTask__.Date = __littleTask.Date;
                    __littleTask__.kM += __littleTask.kM;
                    if (__littleTask__.BetweenBoth >= owner.TimeToMealTicket) //verifica se precisa de VR
                    {
                        __littleTask__.MealTicket = true;

                    }

                }
                listOfLittleTasks.Add(__littleTask__); //adicionou todas as listas de littletask com key como date e between hours como total sum
               

            }

            order.NormalHours = 0.00;
            order.OnTheRach = 0.00;
            order.Overtime = 0.00;
            order.MealTicket = 0;
            order.AmountOfHours = 0.00;
            order.TotalEarned = 0.00;
            _context.Update(order);
            _context.SaveChanges();

            foreach (var little in listOfLittleTasks)
            {
                if(little.BetweenBoth > owner.NormalTime)
                {
                    var gap = little.BetweenBoth - owner.NormalTime;
                    order.Overtime += gap;
                    order.NormalHours += little.BetweenBoth - gap;
                    order.AmountOfHours += little.BetweenBoth;

                }

                else
                {
                    order.NormalHours += little.BetweenBoth;
                    order.AmountOfHours += little.BetweenBoth;
                }
                order.OnTheRach += little.kM;

                if (little.MealTicket == true)
                {
                    order.MealTicket++;
                    order.TotalEarned += owner.MealTicket;
                    
                }

            }
            order.MealTicketValue = order.MealTicket * owner.MealTicket;
            order.TotalEarned += order.NormalHours * owner.PricePerHour;
            order.TotalEarned += order.Overtime * owner.PricePerHour * owner.OvertimeProfitRate;
            order.TotalEarned += order.OnTheRach * owner.GasPrice;

            _context.Update(order);
            _context.SaveChanges();
        }



       
    }
}
