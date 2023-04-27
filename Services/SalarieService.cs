using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class SalarieService : ISalarieService
    {
        private readonly ApplicationDbContext _context;

        public SalarieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salarie>> GetAllSalariesAsync()
        {
            return await _context.Salaries.ToListAsync();
        }

        public async Task<Salarie> GetSalarieByIdAsync(int id)
        {
            return await _context.Salaries.FindAsync(id);
        }

        public async Task CreateSalarieAsync(Salarie Salarie)
        {
            _context.Add(Salarie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalarieAsync(Salarie Salarie)
        {
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
