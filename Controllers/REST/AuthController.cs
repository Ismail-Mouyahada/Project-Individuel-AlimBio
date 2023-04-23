using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AlimBio.Controllers.REST
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
          private readonly UserManager<IdentityUser> _userManager;

        private readonly IConfiguration _configuration;


        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }

         [HttpPost]
        public IActionResult Connexion(LoginDto utilisateur)
        {
            if (_userManager.Users == null)
            {
                throw new ArgumentNullException(nameof(_userManager.Users) + "tout est vide.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            // Check if the user Existes
            var user = _userManager.Users.FirstOrDefault(u => u.Email == utilisateur.Email && u.PasswordHash ==  HashPassword(utilisateur.PasswordHash));
            if (user == null)
            {
                return Unauthorized(new { message = "Déoslé les identifiants invalides ou introuvable"});
            }

            // Create token
            if (utilisateur.Email == null)
            {
                throw new ArgumentNullException(nameof(utilisateur.Email), "tout est vide.");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:APIKey"]));


            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Today.AddDays(2),
                signingCredentials: signingCredentials
                );
            return Ok(new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Date_Expiration = DateTime.Now.AddDays(2),
                Nom_uilisateur = utilisateur.Email,

            });
        }
         public static string HashPassword(string password)
            {
                byte[] salt;
                byte[] buffer2;
                if (password == null)
                {
                    throw new ArgumentNullException("password");
                }
                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
                {
                    salt = bytes.Salt;
                    buffer2 = bytes.GetBytes(0x20);
                }
                byte[] dst = new byte[0x31];
                Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
                Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
                return Convert.ToBase64String(dst);
            }



    }


  public class LoginDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

}
