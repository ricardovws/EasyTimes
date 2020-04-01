﻿using System;
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
        public async Task<IActionResult> Edit(int id, [Bind("id,SerialCode,ClientID,StartDate,EndDate,AmountOfHours")] ServiceOrder serviceOrder)
        {
            if (id != serviceOrder.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceOrder);
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
                date = DateTime.Now;
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
            
            LittleTask littleTask = new LittleTask();
            littleTask.Start = start;
            littleTask.End = end;
            littleTask.BetweenBoth = workload;
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
            littleTask.Date = little.Date;
            var workload = little.End.Subtract(little.Start).TotalHours;
            littleTask.BetweenBoth = workload;
            
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
            var id = list.First().id;
            var _list = _context.LittleTask.Where(l => l.ServiceOrderID == id).ToList();

            dynamic myModel = new ExpandoObject();
            myModel._list = _list;
         

            ReportToPrintViewModel reportToPrintViewModel = new ReportToPrintViewModel();
            reportToPrintViewModel._list = myModel._list;


            return View(reportToPrintViewModel);

        }
    }
}
