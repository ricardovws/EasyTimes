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
    public class LittleTasksController : Controller
    {
        private readonly EasyTimesContext _context;

        public LittleTasksController(EasyTimesContext context)
        {
            _context = context;
        }

        // GET: LittleTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.LittleTask.ToListAsync());
        }

        // GET: LittleTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var littleTask = await _context.LittleTask
                .FirstOrDefaultAsync(m => m.id == id);
            if (littleTask == null)
            {
                return NotFound();
            }

            return View(littleTask);
        }

        // GET: LittleTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LittleTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ServiceOrderID,Json")] LittleTask littleTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(littleTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(littleTask);
        }

        // GET: LittleTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var littleTask = await _context.LittleTask.FindAsync(id);
            if (littleTask == null)
            {
                return NotFound();
            }
            return View(littleTask);
        }

        // POST: LittleTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ServiceOrderID,Json")] LittleTask littleTask)
        {
            if (id != littleTask.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(littleTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LittleTaskExists(littleTask.id))
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
            return View(littleTask);
        }

        // GET: LittleTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var littleTask = await _context.LittleTask
                .FirstOrDefaultAsync(m => m.id == id);
            if (littleTask == null)
            {
                return NotFound();
            }

            return View(littleTask);
        }

        // POST: LittleTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var littleTask = await _context.LittleTask.FindAsync(id);
            _context.LittleTask.Remove(littleTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LittleTaskExists(int id)
        {
            return _context.LittleTask.Any(e => e.id == id);
        }
    }
}
