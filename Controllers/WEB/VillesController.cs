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
    public class VillesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Villes
        public async Task<IActionResult> Index()
        {
            return _context.Villes != null ?
                        View(await _context.Villes.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Villes'  is null.");
        }

        // GET: Villes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Villes == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ville == null)
            {
                return NotFound();
            }

            return View(ville);
        }

        // GET: Villes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Villes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomVille,CodePostal")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ville);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ville);
        }

        // GET: Villes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Villes == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes.FindAsync(id);
            if (ville == null)
            {
                return NotFound();
            }
            return View(ville);
        }

        // POST: Villes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomVille,CodePostal")] Ville ville)
        {
            if (id != ville.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ville);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VilleExists(ville.Id))
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
            return View(ville);
        }

        // GET: Villes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Villes == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ville == null)
            {
                return NotFound();
            }

            return View(ville);
        }

        // POST: Villes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Villes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Villes'  is null.");
            }
            var ville = await _context.Villes.FindAsync(id);
            if (ville != null)
            {
                _context.Villes.Remove(ville);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VilleExists(int id)
        {
            return (_context.Villes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
