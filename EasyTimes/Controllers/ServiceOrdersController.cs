using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;
using Newtonsoft.Json;
using EasyTimes.Services;
using EasyTimes.Models.ViewModels;
using System.Dynamic;
using System.Globalization;

namespace EasyTimes.Controllers
{
    public class ServiceOrdersController : Controller
    {
        private readonly EasyTimesContext _context;
        private readonly ServiceOrderService _serviceOrderService;

        public ServiceOrdersController(EasyTimesContext context, ServiceOrderService serviceOrderService)
        {
            _context = context;
            _serviceOrderService = serviceOrderService;
        }



        // GET: ServiceOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceOrder.OrderByDescending(x=>x.id).ToListAsync());
        }

        // GET: ServiceOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceOrder = await _context.ServiceOrder
                .FirstOrDefaultAsync(m => m.id == id);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        // GET: ServiceOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,SerialCode,ClientID,StartDate,EndDate,AmountOfHours")] ServiceOrder serviceOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceOrder);
        }

        // GET: ServiceOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceOrder = await _context.ServiceOrder.FindAsync(id);
            if (serviceOrder == null)
            {
                return NotFound();
            }
            return View(serviceOrder);
        }

        // POST: ServiceOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceOrder serviceOrder)
        {
            if (id != serviceOrder.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var service_Order =_context.ServiceOrder.First(x => x.id == id);
                    service_Order.ProjectName = serviceOrder.ProjectName;
                    service_Order.Comments = serviceOrder.Comments;
                    service_Order.PaymentStatus = serviceOrder.PaymentStatus;
                    _context.Update(service_Order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceOrderExists(serviceOrder.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(serviceOrder);
        }

        // GET: ServiceOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceOrder = await _context.ServiceOrder
                .FirstOrDefaultAsync(m => m.id == id);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        // POST: ServiceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceOrder = await _context.ServiceOrder.FindAsync(id);
            _context.ServiceOrder.Remove(serviceOrder);
            var list =_context.LittleTask.Where(x => x.ServiceOrderID == id).ToList();
            foreach(var line in list)
            {
                _context.LittleTask.Remove(line);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceOrderExists(int id)
        {
            return _context.ServiceOrder.Any(e => e.id == id);
        }

        public IActionResult ViewDetails(int id)
        {
            var order = _context.ServiceOrder.Where(o => o.id == id).First();

            var list = _context.ServiceOrder.Where(x => x.CheckIn == true);
            foreach(var line in list)
            {
                line.CheckIn = false;
                _context.ServiceOrder.Update(line);
            }
            
            order.CheckIn = true;
            _context.ServiceOrder.Update(order);
            _context.SaveChanges();
            return View(order);

        }

      
        public IActionResult AddWorkload(int id, DateTime date, DateTime start, DateTime end, double kM)


        {
            if(date == DateTime.MinValue)
            {
                date = DateTime.Now.Date;
            }
            if(start == DateTime.MinValue)
            {
                start = DateTime.Now; 
            }
            if (end == DateTime.MinValue)
            {
                end = DateTime.Now.AddHours(1);
            }
            var workload = end.Subtract(start).TotalHours;

            TempData["date"] = date.ToString("yyyy-MM-dd");
            TempData["start"] = start.ToString("hh:mm");
            TempData["end"] = end.ToString("hh:mm");

            int iD = 0;

            if (_context.LittleTask.Any() == false)
            {
                iD = 1;
            }
            else
            {
                var ID = _context.LittleTask.Last(x => x.id != 0).id;
                ID++;
                iD = ID;

            }



            LittleTask littleTask = new LittleTask();
            littleTask.id = iD;
            littleTask.Start = start;
            littleTask.End = end;
            //var JAJA = Convert.ToDouble(workload, new CultureInfo("pt-BR"));
            littleTask.BetweenBoth = Convert.ToDouble(workload.ToString("N2"));
            littleTask.kM = kM;
            littleTask.ServiceOrderID = id;
            littleTask.Date = date;
            _context.LittleTask.Add(littleTask);
            _context.SaveChanges();
          
            //roda método pra atualizar a service order
            _serviceOrderService.TotalValues(id);
            //var json = JsonConvert.SerializeObject(charger);
            var order = _context.ServiceOrder.Where(s => s.id == id).First();
            return RedirectToAction(nameof(ViewDetails), new { id = order.id });
            
        }
        public IActionResult ShowListOfDays(int id)
        {
           
            var list = _context.LittleTask.OrderByDescending(n=>n.Date).Where(x => x.ServiceOrderID == id).ToList();
            foreach(var line in list)
            {
               line.Date_string=line.Date.ToString("dd/MM/yyyy");
               line.Start_string = line.Start.ToString("HH:mm");
               line.End_string = line.End.ToString("HH:mm");
            }
            return View(list);
        }


        //GET
        public IActionResult EditLittleTask(int id)
        {
            var little = _context.LittleTask.Where(l => l.id == id).First();
            return View(little);
            
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLittleTask(LittleTask little)
        {
            var littleTask = _context.LittleTask.Where(l => l.id == little.id).First();
            littleTask.Start = little.Start;
            littleTask.End = little.End;
            littleTask.kM = little.kM;
            
            var workload = little.End.Subtract(little.Start).TotalHours;
            littleTask.BetweenBoth = Convert.ToDouble(workload.ToString("N2"));

            var serviceOrderId = littleTask.ServiceOrderID;
            _serviceOrderService.TotalValues(serviceOrderId);
            return GoBackToListOfDays();

        }

        public IActionResult GoBackToListOfDays()
        {
            var list = _context.ServiceOrder.Where(s => s.CheckIn == true).ToList();
            var id = list.First().id;
            return RedirectToAction(nameof(ShowListOfDays), new { id = id });
        }

        public IActionResult GoBackToDetails()
        {
            var list = _context.ServiceOrder.Where(s => s.CheckIn == true).ToList();
            var id = list.First().id;
            return RedirectToAction(nameof(ViewDetails), new { id = id });
        }

        //GET
        public IActionResult DeleteLittleTask(int id)
        {
            var little = _context.LittleTask.Where(l => l.id == id).First();
            return View(little);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLittleTask(LittleTask little)
        {
            var littleTask = _context.LittleTask.Where(l => l.id == little.id).First();
            _context.LittleTask.Remove(littleTask);
            _context.SaveChanges();
            var serviceOrderId = littleTask.ServiceOrderID;
            _serviceOrderService.TotalValues(serviceOrderId);
            return GoBackToListOfDays();

        }

        public IActionResult ExportFileToPrint()
        {
            var list = _context.ServiceOrder.Where(s => s.CheckIn == true).ToList();
            var serviceOrder = list.First();
            var id = serviceOrder.id;
            var _list = _context.LittleTask.Where(l => l.ServiceOrderID == id).ToList();
            var comments = serviceOrder.Comments;
            var amountOfHours = serviceOrder.AmountOfHours;
            var overtime = serviceOrder.Overtime;
            var mealTicket = serviceOrder.MealTicketValue;
            var onTheRanch = serviceOrder.OnTheRach;
            var totalEarned = serviceOrder.TotalEarned;
            var projectName = serviceOrder.ProjectName;
            var serialCode = serviceOrder.SerialCode;


            dynamic myModel = new ExpandoObject();
            myModel._list = _list;
         

            ReportToPrintViewModel reportToPrintViewModel = new ReportToPrintViewModel();
            reportToPrintViewModel.ProjectName = projectName;
            reportToPrintViewModel.SerialCode = serialCode;
            reportToPrintViewModel._list = myModel._list;
            reportToPrintViewModel.Comments = comments;
            reportToPrintViewModel.AmountOfHours = amountOfHours;
            reportToPrintViewModel.Overtime = overtime;
            reportToPrintViewModel.MealTicketValue = mealTicket;
            reportToPrintViewModel.OnTheRach=onTheRanch;
            reportToPrintViewModel.TotalEarned = totalEarned;

            return View(reportToPrintViewModel);

        }


        //GET
        public IActionResult PaymentStatus(int id)
        {
            var status = _context.ServiceOrder.Where(l => l.id == id).First();
            return View(status);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentStatus(ServiceOrder status)
        {
            var _status = _context.ServiceOrder.Where(l => l.id == status.id).First();
            _status.PaymentStatus = status.PaymentStatus;
            _context.ServiceOrder.Update(_status);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
