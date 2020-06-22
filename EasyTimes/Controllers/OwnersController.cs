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
            var profile = new EditProfileViewModel();
            var owner = _context.Owner.FirstOrDefault(x => x.Name != null);
            profile.id = owner.id;
            profile.Name = owner.Name;
            profile.Email = owner.Email;
            profile.Phone = owner.Phone;
            profile.Bank = owner.Bank;
            profile.Agency = owner.Agency;
            profile.CurrentAccount = owner.CurrentAccount;
            profile.PricePerHour = owner.PricePerHour.ToString();
            profile.GasPrice = owner.GasPrice.ToString();
            profile.OvertimeProfitRate = owner.OvertimeProfitRate.ToString();
            profile.NormalTime = owner.NormalTime.ToString();
            profile.TimeToMealTicket = owner.TimeToMealTicket.ToString();
            profile.MealTicket = owner.MealTicket.ToString();

            return View(profile);
           
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


       
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public void Edit(string infoToEdit, string newInfo)
        {
            int id = 1;
            var owner = _context.Owner.First(x => x.id == id);

            var info = infoToEdit.ToString();
            info = info.Remove(0, 1);

            if (info == "Name")
            {
                owner.Name = newInfo.ToString();
            }
            if (info == "Email")
            {
                owner.Email = newInfo.ToString();
            }
            if (info == "Phone")
            {
                owner.Phone = newInfo.ToString();
            }
            if (info == "Bank")
            {
                owner.Bank = newInfo.ToString();
            }
            if (info == "Agency")
            {
                owner.Agency = newInfo;
            }
            if (info == "CurrentAccount")
            {
                owner.CurrentAccount = newInfo.ToString();
            }

            _context.Owner.Update(owner);
            _context.SaveChanges();

            var _newInfo = Convert.ToDouble(newInfo, System.Globalization.CultureInfo.InvariantCulture);

            if (_newInfo > 0)
            {
                if (info == "PricePerHour")
                {
                    owner.PricePerHour = _newInfo;
                }
                if (info == "GasPrice")
                {
                    owner.GasPrice = _newInfo;
                }
                if (info == "NormalTime")
                {
                    owner.NormalTime = _newInfo;
                }
                if (info == "TimeToReceiveMealTicket")
                {
                    owner.TimeToMealTicket = _newInfo;
                }
                if (info == "MealTicket")
                {
                    owner.MealTicket = _newInfo;
                }
                if (info == "OvertimeProfitRate")
                {
                    owner.OvertimeProfitRate = _newInfo;
                }
            }

            _context.Owner.Update(owner);
            _context.SaveChanges();
                 
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
