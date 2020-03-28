using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;
using Newtonsoft.Json;

namespace EasyTimes.Controllers
{
    public class ServiceOrdersController : Controller
    {
        private readonly EasyTimesContext _context;

        public ServiceOrdersController(EasyTimesContext context)
        {
            _context = context;
        }

        // GET: ServiceOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceOrder.ToListAsync());
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

      
        public IActionResult AddWorkload(DateTime date, DateTime start, DateTime end)
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
            
            var order = _context.ServiceOrder.Where(s => s.CheckIn == true).First();
            order.AmountOfHours += workload;

            Charger charger = new Charger { Start_ = start, End_ = end, Hours_ = workload };

            //serializa o charger
            var json = JsonConvert.SerializeObject(charger);

            LittleTask littleTask = new LittleTask { ServiceOrderID = order.id, Json = json };

            _context.LittleTask.Add(littleTask);
            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(ViewDetails), new { id = order.id });
            
        }
    }
}
