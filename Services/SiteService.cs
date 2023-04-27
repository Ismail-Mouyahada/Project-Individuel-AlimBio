using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            try
            {
                return await _context.Sites
                    .Include(s => s.Entreprise)
                    .Include(s => s.Ville)
                    .FirstOrDefaultAsync(s => s.Id == id)
                    ?? throw new SiteNotFoundException($"Site with Id {id} not found");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while getting site with ID {id}");
                throw;
            }
        }

        public async Task<List<Entreprise>> GetEntreprisesAsync()
        {
            return await _context.Entreprises.ToListAsync();
        }

        public async Task<List<Site>> GetSitesAsync()
        {
            return await _context.Sites.ToListAsync();
        }

        public async Task CreateSiteAsync(Site site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteAsync(Site site)
        {
            try
            {
                var existingSite = await _context.Sites.FindAsync(site.Id);
                if (existingSite == null)
                {
                    throw new SiteNotFoundException();
                }

                existingSite.Siret = site.Siret;
                existingSite.NomSite = site.NomSite;
                existingSite.Description = site.Description;
                existingSite.TypeSite = site.TypeSite;
                existingSite.Statut = site.Statut;
                existingSite.NombreEmployees = site.NombreEmployees;
                existingSite.Capital = site.Capital;
                existingSite.Adresse = site.Adresse;
                existingSite.Langitude = site.Langitude;
                existingSite.Largitude = site.Largitude;
                existingSite.Effectif = site.Effectif;
                existingSite.DateCreation = site.DateCreation;
                existingSite.Tel = site.Tel;
                existingSite.Email = site.Email;
                existingSite.SiteWeb = site.SiteWeb;
                existingSite.EntrepriseId = site.EntrepriseId;
                existingSite.VilleId = site.VilleId;

                _context.Sites.Update(existingSite);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while updating site with ID {site.Id}");
                throw;
            }
        }

        public async Task DeleteSiteAsync(int id)
        {
            try
            {
                var site = await _context.Sites.FindAsync(id);
                if (site == null)
                {
                    throw new SiteNotFoundException();
                }

                _context.Sites.Remove(site);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while deleting site with ID {id}");
                throw;
            }
        }
    }

    [Serializable]
    public class SiteNotFoundException : Exception
    {
        public SiteNotFoundException()
        {
        }

        public SiteNotFoundException(string message) : base(message)
        {
        }

        public SiteNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }



}
