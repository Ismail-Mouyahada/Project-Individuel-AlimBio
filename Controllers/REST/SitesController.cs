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
    public class SitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites()
        {
            if (_context.Sites == null)
            {
                return NotFound();
            }
            return await _context.Sites.ToListAsync();
        }

        // GET: api/Sites/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Site>> GetSite(int id)
        {
            if (_context.Sites == null)
            {
                return NotFound();
            }
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // PUT: api/Sites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(int id, Site site)
        {
            if (id != site.Id)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(Site site)
        {
            if (_context.Sites == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sites'  is null.");
            }
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSite", new { id = site.Id }, site);
        }

        // DELETE: api/Sites/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            if (_context.Sites == null)
            {
                return NotFound();
            }
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return (_context.Sites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
