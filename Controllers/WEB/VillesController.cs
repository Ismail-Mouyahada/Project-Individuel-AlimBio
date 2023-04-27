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
    public class VillesController : Controller
    {
        private readonly IVilleService _villeService;

        public VillesController(IVilleService villeService)
        {
            _villeService = villeService;
        }

        // GET: Villes
        public async Task<IActionResult> Index()
        {
            var villes = await _villeService.GetAllVillesAsync();
            return View(villes);
        }

        // GET: Villes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ville = await _villeService.GetVilleByIdAsync(id.Value);

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
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomVille,CodePostal")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                await _villeService.CreateVilleAsync(ville);
                return RedirectToAction(nameof(Index));
            }
            return View(ville);
        }

        // GET: Villes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ville = await _villeService.GetVilleByIdAsync(id.Value);

            if (ville == null)
            {
                return NotFound();
            }

            return View(ville);
        }

        // POST: Villes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
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
                    await _villeService.UpdateVilleAsync(ville);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VilleExists(ville.Id))
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

        private Task<bool> VilleExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Villes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ville = await _villeService.GetVilleByIdAsync(id.Value);

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
            var ville = await _villeService.GetVilleByIdAsync(id);

            if (ville == null)
            {
                return NotFound();
            }

            await _villeService.DeleteVilleAsync(ville.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
