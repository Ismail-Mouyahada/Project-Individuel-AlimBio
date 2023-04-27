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
        Task<Salarie> GetSalarieByIdAsync(int id);
        Task CreateSalarieAsync(Salarie Salarie);
        Task UpdateSalarieAsync(Salarie Salarie);
        Task DeleteSalarieAsync(int id);

    }
}
