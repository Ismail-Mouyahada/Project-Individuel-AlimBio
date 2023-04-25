using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlimBio.Controllers.WEB
{
    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sites.Include(s => s.Entreprise).Include(s => s.Ville);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sites == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .Include(s => s.Entreprise)
                .Include(s => s.Ville)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise");
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "NomVille");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Siret,NomSite,Description,TypeSite,Statut,NombreEmployees,Capital,Adresse,Langitude,Largitude,Effectif,DateCreation,Tel,Email,SiteWeb,EntrepriseId,VilleId")] Site site)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", site.EntrepriseId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "NomVille", site.VilleId);
            return View(site);
        }

        // GET: Sites/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sites == null)
            {
                return NotFound();
            }

            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", site.EntrepriseId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "NomVille", site.VilleId);
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Siret,NomSite,Description,TypeSite,Statut,NombreEmployees,Capital,Adresse,Langitude,Largitude,Effectif,DateCreation,Tel,Email,SiteWeb,EntrepriseId,VilleId")] Site site)
        {
            if (id != site.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteExists(site.Id))
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", site.EntrepriseId);
            ViewData["VilleId"] = new SelectList(_context.Villes, "Id", "NomVille", site.VilleId);
            return View(site);
        }

        [Authorize]
        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sites == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .Include(s => s.Entreprise)
                .Include(s => s.Ville)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sites == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }
            if (site != null)
            {
                _context.Sites.Remove(site);
            }

            if (_context.Salaries.Any(s => s.SiteId == id))
            {
                TempData["erreur"] = "Vous ne pouvez pas supprimer ce site, parce qu'il est associé avec des salariés.";
                return RedirectToAction(nameof(Index));
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            TempData["reussi"] = "le site a été supprimé avec succès.";
            return RedirectToAction(nameof(Index));
        }

        private bool SiteExists(int id)
        {
            return (_context.Sites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
