using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task CreateServiceAsync(Service Service);
        Task UpdateServiceAsync(Service Service);
        Task DeleteServiceAsync(int id);
    }
}
