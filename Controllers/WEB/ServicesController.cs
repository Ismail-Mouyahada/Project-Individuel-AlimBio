using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AlimBio.Data;
using AlimBio.Models;
using AlimBio.Services;
using Microsoft.EntityFrameworkCore;

namespace AlimBio.Controllers.WEB
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _ServiceService;
        private readonly ISalarieService _SalarieService;

        public ServicesController(IServiceService ServiceService, ISalarieService SalarieService)
        {
            _ServiceService = ServiceService;
            _SalarieService = SalarieService;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var Services = await _ServiceService.GetAllServicesAsync();
            return View(Services);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Service = await _ServiceService.GetServiceByIdAsync(id.Value);

            if (Service == null)
            {
                return NotFound();
            }

            return View(Service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomService,CodePostal")] Service Service)
        {
            if (ModelState.IsValid)
            {
                await _ServiceService.CreateServiceAsync(Service);
                return RedirectToAction(nameof(Index));
            }
            return View(Service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Service = await _ServiceService.GetServiceByIdAsync(id.Value);

            if (Service == null)
            {
                return NotFound();
            }

            return View(Service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomService,CodePostal")] Service Service)
        {
            if (id != Service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ServiceService.UpdateServiceAsync(Service);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ServiceExists(Service.Id))
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
            return View(Service);
        }

        private Task<bool> ServiceExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Service = await _ServiceService.GetServiceByIdAsync(id.Value);

            if (Service == null)
            {
                return NotFound();
            }

            return View(Service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Service = await _ServiceService.GetServiceByIdAsync(id);
            var Salaries = _SalarieService.GetAllSalariesAsync().Result;
            if (Salaries.Any(s => s.ServiceId == id))
            {
                TempData["erreur"] = "Vous ne pouvez pas supprimer ce service , parce que il est associer à des salarié.";
                return RedirectToAction(nameof(Index));
            }

            if (Service == null)
            {

                return RedirectToAction(nameof(Index));
            }

            await _ServiceService.DeleteServiceAsync(Service.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
