using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class EntrepriseService : IEntrepriseService
    {
        private readonly ApplicationDbContext _context;

        public EntrepriseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entreprise>> GetAllEntreprisesAsync()
        {
            return await _context.Entreprises.ToListAsync();
        }

        public async Task<Entreprise> GetEntrepriseByIdAsync(int id)
        {
            return await _context.Entreprises.FindAsync(id);
        }

        public async Task CreateEntrepriseAsync(Entreprise Entreprise)
        {
            _context.Add(Entreprise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntrepriseAsync(Entreprise Entreprise)
        {
            _context.Update(Entreprise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntrepriseAsync(int id)
        {
            var Entreprise = await _context.Entreprises.FindAsync(id);
            if (Entreprise != null)
            {
                _context.Entreprises.Remove(Entreprise);
                await _context.SaveChangesAsync();
            }
        }
    }

}
