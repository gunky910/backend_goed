using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using angularAPI.Models;

namespace backend_herexamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StemController : ControllerBase
    {
        private readonly Context _context;

        public StemController(Context context)
        {
            _context = context;
        }

        // GET: api/stem/5
        [HttpGet("GetStemmenByGebruiker/{id}")]
        public async Task<IEnumerable<Stem>> GetStemmenByGebruiker(int id)
        {
            var stemmen = await _context.Stemmen
                .Where(l => l.gebruikerID == id).ToListAsync();
            return stemmen;
        }
        // GET: api/Stem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmen()
        {
            return await _context.Stemmen.ToListAsync();
        }

        // GET: api/Stem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stem>> GetStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }

        // PUT: api/Stem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem(int id, Stem stem)
        {
            if (id != stem.stemID)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stem
        [HttpPost]
        public async Task<ActionResult<Stem>> PostStem(Stem stem)
        {
            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.stemID }, stem);
        }

        // DELETE: api/Stem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stem>> DeleteStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return stem;
        }

        private bool StemExists(int id)
        {
            return _context.Stemmen.Any(e => e.stemID == id);
        }
    }
}
