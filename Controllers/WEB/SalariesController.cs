using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlimBio.Data;
using AlimBio.Models;

namespace AlimBio.Controllers.WEB
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salaries.Include(s => s.Entreprise).Include(s => s.Service).Include(s => s.Site);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries
                .Include(s => s.Entreprise)
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salarie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Id", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", salarie.SiteId);
            return View(salarie);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries.FindAsync(id);
            if (salarie == null)
            {
                return NotFound();
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Id", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", salarie.SiteId);
            return View(salarie);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie)
        {
            if (id != salarie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salarie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarieExists(salarie.Id))
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Id", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", salarie.SiteId);
            return View(salarie);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries
                .Include(s => s.Entreprise)
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salaries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Salaries'  is null.");
            }
            var salarie = await _context.Salaries.FindAsync(id);
            if (salarie != null)
            {
                _context.Salaries.Remove(salarie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarieExists(int id)
        {
          return (_context.Salaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
