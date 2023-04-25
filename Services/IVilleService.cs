using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface IVilleService
    {
        Task<IEnumerable<Ville>> GetAllVillesAsync();
        Task<Ville> GetVilleByIdAsync(int id);
        Task CreateVilleAsync(Ville ville);
        Task UpdateVilleAsync(Ville ville);
        Task DeleteVilleAsync(int id);
    }
}
