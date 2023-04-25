using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;

namespace AlimBio.Services
{
    public interface IEntrepriseService
    {
        Task<IEnumerable<Entreprise>> GetAllEntreprisesAsync();
        Task<Entreprise> GetEntrepriseByIdAsync(int id);
        Task CreateEntrepriseAsync(Entreprise Entreprise);
        Task UpdateEntrepriseAsync(Entreprise Entreprise);
        Task DeleteEntrepriseAsync(int id);
    }
}
