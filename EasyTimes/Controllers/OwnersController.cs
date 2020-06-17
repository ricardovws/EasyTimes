using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyTimes.Models;
using EasyTimes.Models.ViewModels;

namespace EasyTimes.Controllers
{
    //Fazer método de seed para carregar um user genérico.

    //Está tudo funcional, só falta criar uma viewmodel para fazer a edição separadamente em grupos
    //de atributos, e não tudo de uma vez, como está sendo feito agora.


    public class OwnersController : Controller
    {
        private readonly EasyTimesContext _context;

        public OwnersController(EasyTimesContext context)
        {
            _context = context;
        }

        // GET: Owners
        public IActionResult Index()
        {
           return View(_context.Owner.FirstOrDefault(x => x.Name != null));
           
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner
                .FirstOrDefaultAsync(m => m.id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Email,Phone,Bank,Agency,CurrentAccount,PricePerHour,GasPrice,OvertimeProfitRate")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            id = 1;
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            owner = _context.Owner.FirstOrDefault(x => x.Name != null);
            EditProfileViewModel viewModel = new EditProfileViewModel();
            viewModel.id = owner.id;
            //viewModel.Name = owner.Name;
            //viewModel.Email = owner.Email;
            //viewModel.Phone = owner.Phone;
            //viewModel.Bank = owner.Bank;
            //viewModel.Agency = owner.Agency;
            //viewModel.CurrentAccount = owner.CurrentAccount;
            //
            //viewModel.PricePerHour = owner.PricePerHour.ToString();
            //viewModel.GasPrice = owner.GasPrice.ToString();
            //viewModel.OvertimeProfitRate = owner.OvertimeProfitRate.ToString();
            //viewModel.NormalTime = owner.NormalTime.ToString();
            //viewModel.TimeToMealTicket = owner.TimeToMealTicket.ToString();
            //viewModel.MealTicket = owner.MealTicket.ToString();

            return View(viewModel);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProfileViewModel owner)
        {
            
            if (id != owner.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {   
            try
                {
                    var ownerRefreshed = _context.Owner.First(x => x.id == owner.id);

                    var pricePerHour = Convert.ToDouble(owner.PricePerHour, System.Globalization.CultureInfo.InvariantCulture);
                    if(pricePerHour > 0)
                    {
                        ownerRefreshed.PricePerHour = pricePerHour;
                    }
                 
                    var gasPrice = Convert.ToDouble(owner.GasPrice, System.Globalization.CultureInfo.InvariantCulture);
                    if (gasPrice > 0)
                    {
                        ownerRefreshed.GasPrice = gasPrice;
                    }


                    var normalTime= Convert.ToDouble(owner.NormalTime, System.Globalization.CultureInfo.InvariantCulture);
                    if (normalTime > 0)
                    {
                        ownerRefreshed.NormalTime = normalTime;
                    }


                    var timeToMealTicket = Convert.ToDouble(owner.TimeToMealTicket, System.Globalization.CultureInfo.InvariantCulture);
                    if (timeToMealTicket > 0)
                    {
                        ownerRefreshed.TimeToMealTicket = timeToMealTicket;
                    }


                    var mealTicket = Convert.ToDouble(owner.MealTicket, System.Globalization.CultureInfo.InvariantCulture);
                    if (mealTicket > 0)
                    {
                        ownerRefreshed.MealTicket = mealTicket;
                    }


                    var overtimeProfitRate = Convert.ToDouble(owner.OvertimeProfitRate, System.Globalization.CultureInfo.InvariantCulture);
                    if (overtimeProfitRate > 0)
                    {
                        ownerRefreshed.OvertimeProfitRate = overtimeProfitRate;
                    }


                    _context.Update(ownerRefreshed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.id))
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
            return View(owner);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner
                .FirstOrDefaultAsync(m => m.id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owner.FindAsync(id);
            _context.Owner.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owner.Any(e => e.id == id);
        }
    }
}
