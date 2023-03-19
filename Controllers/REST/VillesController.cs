using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlimBio.Data;
using AlimBio.Models;

namespace AlimBio.Controllers.REST
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VillesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Villes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVilles()
        {
          if (_context.Villes == null)
          {
              return NotFound();
          }
            return await _context.Villes.ToListAsync();
        }

        // GET: api/Villes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ville>> GetVille(int id)
        {
          if (_context.Villes == null)
          {
              return NotFound();
          }
            var ville = await _context.Villes.FindAsync(id);

            if (ville == null)
            {
                return NotFound();
            }

            return ville;
        }

        // PUT: api/Villes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVille(int id, Ville ville)
        {
            if (id != ville.Id)
            {
                return BadRequest();
            }

            _context.Entry(ville).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VilleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Villes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ville>> PostVille(Ville ville)
        {
          if (_context.Villes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Villes'  is null.");
          }
            _context.Villes.Add(ville);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVille", new { id = ville.Id }, ville);
        }

        // DELETE: api/Villes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVille(int id)
        {
            if (_context.Villes == null)
            {
                return NotFound();
            }
            var ville = await _context.Villes.FindAsync(id);
            if (ville == null)
            {
                return NotFound();
            }

            _context.Villes.Remove(ville);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VilleExists(int id)
        {
            return (_context.Villes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
