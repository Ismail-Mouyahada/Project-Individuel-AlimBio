using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AlimBio.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurService(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("", Name = "ListeUtilisateurs")]
        public async Task<IEnumerable<IdentityUser>> GetAllUtilisateursAsync()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}", Name = "DetailsUtilisateur")]
        public async Task<IdentityUser> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        [HttpPost("", Name = "CreerUtilisateur")]
        public async Task CreateUtilisateurAsync(IdentityUser Utilisateur)
        {
            _context.Add(Utilisateur);
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id}", Name = "ModifierUtilisateur")]
        public async Task UpdateUtilisateurAsync(IdentityUser Utilisateur)
        {
            _context.Update(Utilisateur);
            await _context.SaveChangesAsync();
        }


        [HttpDelete("{id}", Name = "SupprimerUtilisateur")]
        public async Task DeleteUtilisateurAsync(int id)
        {
            var Utilisateur = await _context.Users.FindAsync(id);
            if (Utilisateur != null)
            {
                _context.Users.Remove(Utilisateur);
                await _context.SaveChangesAsync();
            }
        }
    }

}
