using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface ISiteService
    {

        Task<IEnumerable<Site>> GetAllSitesAsync();
        Task<Site> GetSiteByIdAsync(int id);
        Task CreateSiteAsync(Site Site);
        Task UpdateSiteAsync(Site Site);
        Task DeleteSiteAsync(int id);

    }
}
