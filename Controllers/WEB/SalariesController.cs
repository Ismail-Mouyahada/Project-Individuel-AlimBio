using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using AlimBio.Services;

namespace AlimBio.Controllers.WEB
{
    public class SalariesController : Controller
    {
        private readonly ISalarieService _salarieService;
        private readonly IEntrepriseService _entrepriseService;
        private readonly ISiteService _siteService;
        private readonly IServiceService _serviceService;

        public SalariesController(ISalarieService salarieService, IServiceService serviceService, IEntrepriseService entrepriseService, ISiteService siteService)
        {
            _salarieService = salarieService;
            _entrepriseService = entrepriseService;
            _serviceService = serviceService;
            _siteService = siteService;
        }

        // GET: Salaries
        public async Task<IActionResult> Index(string? searchString)
        {

            if (searchString == null)
            {
                var salaries = await _salarieService.GetAllSalariesAsync();
                return View(salaries.ToList());
            }
            else if (searchString.Length > 1 && searchString != null)
            {
                var salaries = await _salarieService.GetSomeSalariesAsync(searchString);
                return View(salaries.ToList());
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _salarieService.GetSalarieByIdAsync(id.Value);

            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // GET: Salaries/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EntrepriseId"] = new SelectList(_entrepriseService.GetAllEntreprisesAsync().Result, "Id", "NomEntreprise");
            ViewData["ServiceId"] = new SelectList(_serviceService.GetAllServicesAsync().Result, "Id", "NomService");
            ViewData["SiteId"] = new SelectList(_siteService.GetAllSitesAsync().Result, "Id", "NomSite");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                ViewData["EntrepriseId"] = new SelectList(_entrepriseService.GetAllEntreprisesAsync().Result, "Id", salarie.EntrepriseId.ToString());
                ViewData["ServiceId"] = new SelectList(_serviceService.GetAllServicesAsync().Result, "Id", salarie.ServiceId.ToString());
                ViewData["SiteId"] = new SelectList(_siteService.GetAllSitesAsync().Result, "Id", salarie.SiteId.ToString());
                await _salarieService.CreateSalarieAsync(salarie, Image);
                return RedirectToAction(nameof(Index));
            }
            return View(salarie);
        }

        // GET: Salaries/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _salarieService.GetSalarieByIdAsync(id.Value);

            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie, IFormFile? image)
        {
            if (id != salarie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _salarieService.UpdateSalarieAsync(salarie, image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SalarieExists(salarie.Id))
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
            return View(salarie);
        }

        private async Task<bool> SalarieExists(int id)
        {
            var salarie = await _salarieService.GetSalarieByIdAsync(id);
            return salarie != null;
        }

        // GET: Salaries/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _salarieService.GetSalarieByIdAsync(id.Value);

            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salarie = await _salarieService.GetSalarieByIdAsync(id);

            if (salarie == null)
            {
                return NotFound();
            }

            await _salarieService.DeleteSalarieAsync(salarie.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
