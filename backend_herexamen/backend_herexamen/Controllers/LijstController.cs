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
    public class LijstController : ControllerBase
    {
        private readonly Context _context;

        public LijstController(Context context)
        {
            _context = context;
        }

        // GET: api/Lijst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lijst>>> GetLijsten()
        {
            return await _context.Lijsten.ToListAsync();
        }


        // GET: api/Lijst/5
        [HttpGet("GetLijstenByGebruiker/{id}")]
        public async Task<IEnumerable<Lijst>> GetLijstenByGebruiker(int id)
        {
            var lijsten = await _context.Lijsten
                .Where(l => l.gebruikerID == id).ToListAsync();
            return lijsten;
        }

        // GET: api/Lijst/5
        [HttpGet("GetLijstenByStem/{id}")]
        public async Task<IEnumerable<Lijst>> GetLijstenByStem(int id)
        {
            var lijsten = await _context.Lijsten
                .Where(l => l.lijstID == id).ToListAsync();
            return lijsten;
        }

        // GET: api/Lijst/5
        [HttpGet("GetActieveLijsten")]
        public async Task<IEnumerable<Lijst>> GetActieveLijsten()
        {
            var lijsten = await _context.Lijsten
                .Include(l => l.items)
                .ThenInclude(i => i.stemmen)
                .Where(l => l.startDatum <= DateTime.Now && l.eindDatum >= DateTime.Now).ToListAsync();
                
            return lijsten;
        }

        // GET: api/Lijst/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lijst>> GetLijst(int id)
        {
            var lijst = await _context.Lijsten
                .Include(l => l.items)
                .ThenInclude(i => i.stemmen)
                .Where(l => l.lijstID == id)
                .FirstAsync();

            if (lijst == null)
            {
                return NotFound();
            }

            return lijst;
        }

        // PUT: api/Lijst/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLijst(int id, Lijst lijst)
        {
            if (id != lijst.lijstID)
            {
                return BadRequest();
            }

            _context.Entry(lijst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LijstExists(id))
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

        [HttpGet("getLijstGestemmed/{id}")]
        public async Task<IEnumerable<Lijst>> getLijstGestemmed(long id)
        {
            var stemmen = await _context.Stemmen.Where(s => s.gebruikerID == id).ToListAsync();
            List<Lijst> lijsten = new List<Lijst>();
            List<Item> items = new List<Item>();

            foreach (Stem stem in stemmen)
            {
                var item = await _context.Items
                    .Where(i => i.itemID == stem.itemID)
                    .FirstAsync();
                var lijst = await _context.Lijsten
                    .Where(l => l.lijstID == item.lijstID)
                    .Include(l => l.items)
                    .ThenInclude(i => i.stemmen)
                    .FirstAsync();
                lijsten.Add(lijst);
            }

            return lijsten;
        }

        // POST: api/Lijst
        [HttpPost]
        public async Task<ActionResult<Lijst>> PostLijst(Lijst lijst)
        {
            _context.Lijsten.Add(lijst);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLijst", new { id = lijst.lijstID }, lijst);
        }

        // DELETE: api/Lijst/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lijst>> DeleteLijst(int id)
        {
            var lijst = await _context.Lijsten.FindAsync(id);
            if (lijst == null)
            {
                return NotFound();
            }

            _context.Lijsten.Remove(lijst);
            await _context.SaveChangesAsync();

            return lijst;
        }

        private bool LijstExists(int id)
        {
            return _context.Lijsten.Any(e => e.lijstID == id);
        }
    }
}
