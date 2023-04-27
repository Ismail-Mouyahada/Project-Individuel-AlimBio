
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
using Microsoft.AspNetCore.Mvc;

namespace AlimBio.Services
{
    public class SalarieService : ISalarieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SalarieService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Salarie>> GetAllSalariesAsync()
        {
            var salaries = _context.Salaries;

            return await salaries.ToListAsync();

        }

        public async Task<IEnumerable<Salarie>> GetSomeSalariesAsync(string? searchString)
        {
            var salaries = from s in _context.Salaries select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                salaries = salaries.Where(s => s.Nom.Contains(searchString)
                                               || s.Prenom.Contains(searchString) || s.Email.Contains(searchString) || s.Poste.Contains(searchString) || s.Service.NomService.Contains(searchString) || s.Site.NomSite.Contains(searchString));
            }
            return await salaries.ToListAsync();


        }

        public async Task<Salarie> GetSalarieByIdAsync(int id)
        {
            if (id == 0 || _context.Salaries == null)
            {
                return null;
            }

            var salarie = await _context.Salaries
                .Include(s => s.Entreprise)
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);

            return salarie;
        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        public async Task CreateSalarieAsync(Salarie salarie, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Create a unique filename for the image
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

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
                    image.CopyTo(fileStream);
                }

                // Save the path to the image in the database
                salarie.Image = $"/images/{uniqueFileName}";
            }
            else
            {
                salarie.Image = "/images/unconnu.jpeg";
            }

            _context.Add(salarie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalarieAsync(Salarie Salarie, IFormFile image)
        {

            if (image != null && image.Length > 0)
            {
                // Create a unique filename for the image
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

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
                    image.CopyTo(fileStream);
                }

                // Save the path to the image in the database
                Salarie.Image = $"/images/{uniqueFileName}";
            }
            else
            {
                Salarie.Image = "/images/unconnu.jpeg";
            }
            _context.Update(Salarie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalarieAsync(int id)
        {
            var Salarie = await _context.Salaries.FindAsync(id);
            if (Salarie != null)
            {
                _context.Salaries.Remove(Salarie);
                await _context.SaveChangesAsync();
            }
        }


    }

}
