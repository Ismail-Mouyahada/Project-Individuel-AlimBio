using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface ISalarieService
    {

        Task<IEnumerable<Salarie>> GetAllSalariesAsync();
        Task<IEnumerable<Salarie>> GetSomeSalariesAsync(string? searchString = null);
        Task<Salarie> GetSalarieByIdAsync(int id);
        Task CreateSalarieAsync(Salarie Salarie, IFormFile Image);
        Task UpdateSalarieAsync(Salarie salarie, IFormFile image);
        Task DeleteSalarieAsync(int id);

    }
}
