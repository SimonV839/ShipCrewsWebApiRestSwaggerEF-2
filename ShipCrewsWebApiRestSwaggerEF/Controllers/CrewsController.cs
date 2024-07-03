using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewsController : ControllerBase
    {
        private readonly ShipCrewsContext _context;

        public CrewsController(ShipCrewsContext context)
        {
            _context = context;
        }

        // Get : api/Crews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crew>>> GetCrew()
        {
            if (_context.Crews == null)
            {
                return NotFound();
            }
            return await _context.Crews.ToListAsync();
        }

        // Get : api/Crews/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Crew>> GetCrew(int id)
        {
            if (_context.Crews is null)
            {
                return NotFound();
            }
            var crew = await _context.Crews.FindAsync(id);
            if (crew is null)
            {
                return NotFound();
            }
            return crew;
        }

        // Post : api/Crews
        [HttpPost]
        public async Task<ActionResult<Crew>> PostCrew(Crew crew)
        {
            _context.Crews.Add(crew);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCrew), new { id = crew.CrewId }, crew);
        }

        // Put : api/Crews/2
        [HttpPut]
        public async Task<ActionResult<Crew>> PutCrew(int id, Crew crew)
        {
            if (id != crew.CrewId)
            {
                return BadRequest();
            }
            _context.Entry(crew).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        // Delete : api/Crews/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crew>> DeleteCrew(int id)
        {
            if (_context.Crews is null)
            {
                return NotFound();
            }
            var crew = await _context.Crews.FindAsync(id);
            if (crew is null)
            {
                return NotFound();
            }
            _context.Crews.Remove(crew);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CrewExists(long id)
        {
            return (_context.Crews?.Any(crew => crew.CrewId == id)).GetValueOrDefault();
        }

    }
}
