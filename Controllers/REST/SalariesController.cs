using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlimBio.Data;
using AlimBio.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlimBio.Controllers.REST
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Salaries
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salarie>>> GetSalaries()
        {
            if (_context.Salaries == null)
            {
                return NotFound();
            }
            return await _context.Salaries.ToListAsync();
        }

        // GET: api/Salaries/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Salarie>> GetSalarie(int id)
        {
            if (_context.Salaries == null)
            {
                return NotFound();
            }
            var salarie = await _context.Salaries.FindAsync(id);

            if (salarie == null)
            {
                return NotFound();
            }

            return salarie;
        }

        // PUT: api/Salaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalarie(int id, Salarie salarie)
        {
            if (id != salarie.Id)
            {
                return BadRequest();
            }

            _context.Entry(salarie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalarieExists(id))
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

        // POST: api/Salaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Salarie>> PostSalarie(Salarie salarie)
        {
            if (_context.Salaries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Salaries'  is null.");
            }
            _context.Salaries.Add(salarie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalarie", new { id = salarie.Id }, salarie);
        }

        // DELETE: api/Salaries/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalarie(int id)
        {
            if (_context.Salaries == null)
            {
                return NotFound();
            }
            var salarie = await _context.Salaries.FindAsync(id);
            if (salarie == null)
            {
                return NotFound();
            }

            _context.Salaries.Remove(salarie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalarieExists(int id)
        {
            return (_context.Salaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
