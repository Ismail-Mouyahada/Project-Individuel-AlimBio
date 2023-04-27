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
    public class SitesController : Controller
    {
        private readonly ISiteService _SiteService;
        private readonly IEntrepriseService _EntrepriseService;
        private readonly IVilleService _VilleService;

        public SitesController(ISiteService SiteService, IEntrepriseService EntrepriseService, IVilleService VilleService)
        {
            _SiteService = SiteService;
            _EntrepriseService = EntrepriseService;
            _VilleService = VilleService;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var Sites = await _SiteService.GetAllSitesAsync();
            return View(Sites);
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Site = await _SiteService.GetSiteByIdAsync(id.Value);

            if (Site == null)
            {
                return NotFound();
            }

            return View(Site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["EntrepriseId"] = new SelectList(_EntrepriseService.GetAllEntreprisesAsync().Result, "Id", "NomEntreprise");
            ViewData["VilleId"] = new SelectList(_VilleService.GetAllVillesAsync().Result, "Id", "NomVille");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomSite,CodePostal")] Site Site)
        {
            if (ModelState.IsValid)
            {
                await _SiteService.CreateSiteAsync(Site);
                return RedirectToAction(nameof(Index));
            }
            return View(Site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Site = await _SiteService.GetSiteByIdAsync(id.Value);

            if (Site == null)
            {
                return NotFound();
            }

            return View(Site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomSite,CodePostal")] Site Site)
        {
            if (id != Site.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _SiteService.UpdateSiteAsync(Site);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SiteExists(Site.Id))
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
            return View(Site);
        }

        private Task<bool> SiteExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Site = await _SiteService.GetSiteByIdAsync(id.Value);

            if (Site == null)
            {
                return NotFound();
            }

            return View(Site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Site = await _SiteService.GetSiteByIdAsync(id);

            if (Site == null)
            {
                return NotFound();
            }

            await _SiteService.DeleteSiteAsync(Site.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
