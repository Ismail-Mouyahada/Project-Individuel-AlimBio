using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlimBio.Controllers.WEB
{
    [Route("[controller]")]
    public class UtilsiateursController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UtilsiateursController(UserManager<IdentityUser> userManager)
        {
             _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


             var applicationDbContext = _userManager.Users.Include(s => s.UserName).Include(s => s.Email);
            return View(await applicationDbContext.ToListAsync());
        }




        // Action qui ajoute un nouvel utilisateur
            [HttpPost]
            public async Task<IActionResult> Create(string username, string email, string password)
            {
                var user = new IdentityUser { UserName = username, Email = email };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // L'utilisateur a été créé avec succès
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Il y a eu une erreur lors de la création de l'utilisateur
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }
                    // Action qui affiche les détails d'un utilisateur
            public async Task<IActionResult> Details(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return NotFound();
                }
            }

            public async Task<IActionResult> Edit(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return NotFound();
                }
            }

            // Action qui enregistre les modifications d'un utilisateur
            [HttpPost]
            public async Task<IActionResult> Edit(string id, string username, string email)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = username;
                    user.Email = email;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // Les modifications ont été enregistrées avec succès
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Il y a eu une erreur lors de l'enregistrement des modifications
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(user);
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            // Action qui affiche la confirmation de suppression d'un utilisateur
                public async Task<IActionResult> Delete(string id)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        return View(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                // Action qui supprime un utilisateur
                [HttpPost]
                public async Task<IActionResult> DeleteConfirmed(string id)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            // L'utilisateur a été supprimé avec succès
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            // Il y a eu une erreur lors de la suppression de l'utilisateur
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View(user);
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
