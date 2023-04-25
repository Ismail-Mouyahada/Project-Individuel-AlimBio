using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlimBio.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace AlimBio.Services
{
    public interface IUtilisateurService
    {
        Task<IEnumerable<IdentityUser>> GetAllUtilisateursAsync();
        Task<IdentityUser> GetUtilisateurByIdAsync(int id);
        Task CreateUtilisateurAsync(IdentityUser Utilisateur);
        Task UpdateUtilisateurAsync(IdentityUser Utilisateur);
        Task DeleteUtilisateurAsync(int id);
    }
}
