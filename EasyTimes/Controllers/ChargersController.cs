using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;

namespace EasyTimes.Controllers
{
    public class ChargersController : Controller
    {
        private readonly EasyTimesContext _context;

        public ChargersController(EasyTimesContext context)
        {
            _context = context;
        }

        // GET: Chargers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Charger.ToListAsync());
        }

        // GET: Chargers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charger = await _context.Charger
                .FirstOrDefaultAsync(m => m.id == id);
            if (charger == null)
            {
                return NotFound();
            }

            return View(charger);
        }

        // GET: Chargers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chargers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Start_,End_,Hours_,OnTheRanch")] Charger charger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(charger);
        }

        // GET: Chargers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charger = await _context.Charger.FindAsync(id);
            if (charger == null)
            {
                return NotFound();
            }
            return View(charger);
        }

        // POST: Chargers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Start_,End_,Hours_,OnTheRanch")] Charger charger)
        {
            if (id != charger.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChargerExists(charger.id))
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
            return View(charger);
        }

        // GET: Chargers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charger = await _context.Charger
                .FirstOrDefaultAsync(m => m.id == id);
            if (charger == null)
            {
                return NotFound();
            }

            return View(charger);
        }

        // POST: Chargers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charger = await _context.Charger.FindAsync(id);
            _context.Charger.Remove(charger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChargerExists(int id)
        {
            return _context.Charger.Any(e => e.id == id);
        }
    }
}
