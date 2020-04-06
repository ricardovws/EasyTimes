using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;
using EasyTimes.Services;
using EasyTimes.Models.ViewModels;

namespace EasyTimes.Controllers
{
    public class ClientsController : Controller
    {
        private readonly EasyTimesContext _context;

        private readonly ServiceOrderService _serviceOrderService;

        public ClientsController(EasyTimesContext context, ServiceOrderService serviceOrderService)
        {
            _context = context;
            _serviceOrderService = serviceOrderService;
        }



        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,CNPJ,MainContactEmail,Phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Client.Any();
                if(_context.Client.Any() == false)
                {
                    client.id = 1;
                }
                else
                {
                    var id = _context.Client.Last(x => x.id != 0).id;
                    id++;
                    client.id = id;
                    
                }
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,CNPJ,MainContactEmail,Phone")] Client client)
        {
            if (id != client.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.id == id);
        }


        //GET
        public IActionResult StartAService()
        {
            StartAServiceViewModel startAServiceViewModel = new StartAServiceViewModel();
            return View(startAServiceViewModel);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartAService(int id, StartAServiceViewModel startAServiceViewModel)
        {

            int iD = 1;

            if (_context.ServiceOrder.Any() == false)
            {
                iD = 1;
            }
            else
            {
                var ID = _context.ServiceOrder.Last().id;
                iD += ID;

            }

            ServiceOrder serviceOrder = new ServiceOrder();
            serviceOrder.id = iD;

            var length = 8;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            serviceOrder.SerialCode = result;
            serviceOrder.ClientID = id;
            serviceOrder.ClientName=_context.Client.Where(c => c.id == id).First().Name;
            serviceOrder.ProjectName = startAServiceViewModel.ProjectName;
            serviceOrder.Comments = startAServiceViewModel.Comments;
            

            _serviceOrderService.StartAService(serviceOrder);
            return RedirectToAction(nameof(Index));
        }
    }
}
