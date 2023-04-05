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

namespace AlimBio.Controllers.WEB
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SalariesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Salaries
       


        public async Task<IActionResult> Index(string searchString)
        {


            var salaries = from s in _context.Salaries
                select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                salaries = salaries.Where(s => s.Nom.Contains(searchString)
                                               || s.Prenom.Contains(searchString) || s.Email.Contains(searchString)  || s.Poste.Contains(searchString));
            }
            return View(await salaries.ToListAsync());
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NomService");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "NomSite");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Create a unique filename for the image
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;

                    // Get the path to the wwwroot folder
                    var wwwRootPath = _webHostEnvironment.WebRootPath;

                    // Combine the wwwroot path with the unique filename to get the full path to save the image
                    var imagePath = Path.Combine(wwwRootPath, "images", uniqueFileName);

                    // Create the images directory if it does not exist
                    var imagesDirectory = Path.Combine(wwwRootPath, "images");
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    // Save the image to the file system
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }

                    // Save the path to the image in the database
                    salarie.Image = $"/images/{uniqueFileName}";
                }


                _context.Add(salarie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NomService", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "NomSite", salarie.SiteId);

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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NomService", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "NomSite", salarie.SiteId);
            return View(salarie);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Poste,Email,Mobile,Fix,Adresse,CodePostal,Ville,Pays,ServiceId,EntrepriseId,SiteId")] Salarie salarie, IFormFile Image)
        {
            if (id != salarie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Length > 0)
                    {
                        // Create a unique filename for the image
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;

                        // Get the path to the wwwroot folder
                        var wwwRootPath = _webHostEnvironment.WebRootPath;

                        // Combine the wwwroot path with the unique filename to get the full path to save the image
                        var imagePath = Path.Combine(wwwRootPath, "images", uniqueFileName);

                        // Create the images directory if it does not exist
                        var imagesDirectory = Path.Combine(wwwRootPath, "images");
                        if (!Directory.Exists(imagesDirectory))
                        {
                            Directory.CreateDirectory(imagesDirectory);
                        }

                        // Save the image to the file system
                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            Image.CopyTo(fileStream);
                        }

                        // Save the path to the image in the database
                        salarie.Image = $"/images/{uniqueFileName}";
                    }
                    if (Image != null && Image.Length > 0)
                    {
                        // Create a unique filename for the image
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;

                        // Get the path to the wwwroot folder
                        var wwwRootPath = _webHostEnvironment.WebRootPath;

                        // Combine the wwwroot path with the unique filename to get the full path to save the image
                        var imagePath = Path.Combine(wwwRootPath, "images", uniqueFileName);

                        // Create the images directory if it does not exist
                        var imagesDirectory = Path.Combine(wwwRootPath, "images");
                        if (!Directory.Exists(imagesDirectory))
                        {
                            Directory.CreateDirectory(imagesDirectory);
                        }

                        // Save the image to the file system
                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            Image.CopyTo(fileStream);
                        }

                        // Save the path to the image in the database
                        salarie.Image = $"/images/{uniqueFileName}";
                    }
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "NomEntreprise", salarie.EntrepriseId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NomService", salarie.ServiceId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "NomSite", salarie.SiteId);
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
