using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;

        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task CreateServiceAsync(Service Service)
        {
            _context.Add(Service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServiceAsync(Service Service)
        {
            _context.Update(Service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var Service = await _context.Services.FindAsync(id);
            if (Service != null)
            {
                _context.Services.Remove(Service);
                await _context.SaveChangesAsync();
            }
        }
    }

}
