using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class VilleService : IVilleService
    {
        private readonly ApplicationDbContext _context;

        public VilleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ville>> GetAllVillesAsync()
        {
            return await _context.Villes.ToListAsync();
        }

        public async Task<Ville> GetVilleByIdAsync(int id)
        {
            return await _context.Villes.FindAsync(id);
        }

        public async Task CreateVilleAsync(Ville ville)
        {
            _context.Add(ville);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVilleAsync(Ville ville)
        {
            _context.Update(ville);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVilleAsync(int id)
        {
            var ville = await _context.Villes.FindAsync(id);
            if (ville != null)
            {
                _context.Villes.Remove(ville);
                await _context.SaveChangesAsync();
            }
        }
    }

}
