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
    public class EntreprisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntreprisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Entreprises
        public async Task<IActionResult> Index()
        {
              return _context.Entreprises != null ? 
                          View(await _context.Entreprises.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Entreprises'  is null.");
        }

        // GET: Entreprises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entreprise == null)
            {
                return NotFound();
            }

            return View(entreprise);
        }

        // GET: Entreprises/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entreprises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Siren,NomEntreprise,DescriptionActivite,PersonneMoral,SiretSiege,Nic,NumeroVoie,LibelleVoie,ComplementAdresse,AdresseComplet,CodePostal,Ville,Region,Lattitude,Langitude,DateCreation,EntrepriseEmployeuse,Capital,Effectif,InscritRcs,SiteWeb,ImmatriculationTva")] Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entreprise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entreprise);
        }
        [Authorize]
        // GET: Entreprises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises.FindAsync(id);
            if (entreprise == null)
            {
                return NotFound();
            }
            return View(entreprise);
        }

        // POST: Entreprises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Siren,NomEntreprise,DescriptionActivite,PersonneMoral,SiretSiege,Nic,NumeroVoie,LibelleVoie,ComplementAdresse,AdresseComplet,CodePostal,Ville,Region,Lattitude,Langitude,DateCreation,EntrepriseEmployeuse,Capital,Effectif,InscritRcs,SiteWeb,ImmatriculationTva")] Entreprise entreprise)
        {
            if (id != entreprise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entreprise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrepriseExists(entreprise.Id))
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
            return View(entreprise);
        }

        [Authorize]
        // GET: Entreprises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var entreprise = await _context.Entreprises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entreprise == null)
            {
                return NotFound();
            }

            return View(entreprise);
        }

        // POST: Entreprises/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entreprises == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Entreprises'  is null.");
            }
            var entreprise = await _context.Entreprises.FindAsync(id);
            if (entreprise != null)
            {
                _context.Entreprises.Remove(entreprise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrepriseExists(int id)
        {
          return (_context.Entreprises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
