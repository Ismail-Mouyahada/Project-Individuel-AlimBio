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
using Microsoft.AspNetCore.Authorization;

namespace AlimBio.Controllers.WEB
{
    public class EntreprisesController : Controller
    {
        private readonly IEntrepriseService _EntrepriseService;

        public EntreprisesController(IEntrepriseService EntrepriseService)
        {
            _EntrepriseService = EntrepriseService;
        }

        // GET: Entreprises
        public async Task<IActionResult> Index()
        {
            var Entreprises = await _EntrepriseService.GetAllEntreprisesAsync();
            return View(Entreprises);
        }

        // GET: Entreprises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Entreprise = await _EntrepriseService.GetEntrepriseByIdAsync(id.Value);

            if (Entreprise == null)
            {
                return NotFound();
            }

            return View(Entreprise);
        }

        [Authorize]
        // GET: Entreprises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entreprises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomEntreprise,CodePostal")] Entreprise Entreprise)
        {
            if (ModelState.IsValid)
            {
                await _EntrepriseService.CreateEntrepriseAsync(Entreprise);
                return RedirectToAction(nameof(Index));
            }
            return View(Entreprise);
        }

        [Authorize]
        // GET: Entreprises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Entreprise = await _EntrepriseService.GetEntrepriseByIdAsync(id.Value);

            if (Entreprise == null)
            {
                return NotFound();
            }

            return View(Entreprise);
        }

        // POST: Entreprises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see <http://go.microsoft.com/fwlink/?LinkId=317598>.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomEntreprise,CodePostal")] Entreprise Entreprise)
        {
            if (id != Entreprise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _EntrepriseService.UpdateEntrepriseAsync(Entreprise);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EntrepriseExists(Entreprise.Id))
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
            return View(Entreprise);
        }

        private Task<bool> EntrepriseExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Entreprises/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Entreprise = await _EntrepriseService.GetEntrepriseByIdAsync(id.Value);

            if (Entreprise == null)
            {
                return NotFound();
            }

            return View(Entreprise);
        }

        // POST: Entreprises/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Entreprise = await _EntrepriseService.GetEntrepriseByIdAsync(id);

            if (Entreprise == null)
            {
                return NotFound();
            }

            await _EntrepriseService.DeleteEntrepriseAsync(Entreprise.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
