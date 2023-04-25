using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class SiteService : ISiteService
    {
        private readonly ApplicationDbContext _context;

        public SiteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Site>> GetAllSitesAsync()
        {
            return await _context.Sites.ToListAsync();
        }

        public async Task<Site> GetSiteByIdAsync(int id)
        {
            return await _context.Sites.FindAsync(id);
        }

        public async Task CreateSiteAsync(Site Site)
        {
            _context.Add(Site);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteAsync(Site Site)
        {
            _context.Update(Site);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSiteAsync(int id)
        {
            var Site = await _context.Sites.FindAsync(id);
            if (Site != null)
            {
                _context.Sites.Remove(Site);
                await _context.SaveChangesAsync();
            }
        }
    }

}
