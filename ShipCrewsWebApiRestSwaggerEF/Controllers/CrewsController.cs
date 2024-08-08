using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimonV839.ShipCrewsWebApiRestSwaggerEF.HackedModels;
using SimonV839.ShipCrewsWebApiRestSwaggerEF.Models;

namespace SimonV839.ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewsController : ShipCrewsControllerBase<CrewsController>
    {
        public CrewsController(ILogger<CrewsController> logger, ShipCrewsContext context)
            : base(logger, context)
        {
        }

        // Get : api/Crews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewHacked>>> GetCrew()
        {
            if (Context.Crews == null)
            {
                Logger.LogError(@"{func} called but there is no {table} table", nameof(GetCrew), nameof(Context.Crews));
                return NotFound();
            }

            var efCrews = await Context.Crews.ToListAsync();
            return efCrews.Select(ef => new CrewHacked(ef)).ToList();
        }

        // Get : api/Crews/2
        [HttpGet("{id}")]
        public async Task<ActionResult<CrewHacked>> GetCrew(int id)
        {
            if (Context.Crews is null)
            {
                Logger.LogError(@"{func} called with {id} but there is no {table} table", nameof(GetCrew), id, nameof(Context.Crews));
                return NotFound();
            }
            var crew = await Context.Crews.FindAsync(id);
            if (crew is null)
            {
                Logger.LogInformation(@"{func} called with {id} but this is not present in {table} table", nameof(GetCrew), id, nameof(Context.Crews));
                return NotFound();
            }
            return new CrewHacked(crew);
        }

        // Post : api/Crews
        [HttpPost]
        public async Task<ActionResult<CrewHacked>> PostCrew(CrewHacked crew)
        {
            var efCrew = crew.ToCrew();
            Context.Crews.Add(efCrew);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostCrew), new { id = efCrew.CrewId }, new CrewHacked(efCrew));
        }

        // Put : api/Crews/2
        [HttpPut]
        public async Task<ActionResult<CrewHacked>> PutCrew(int id, CrewHacked crew)
        {
            if (id != crew.CrewId)
            {
                Logger.LogError(@"{func} called with {id} but this does match the value supplied in {crew}", nameof(PutCrew), id, crew);
                return BadRequest();
            }
            var efCrew = await Context.Crews.FindAsync(id);
            if (efCrew is null)
            {
                Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(PutCrew), id);
                return NotFound();
            }
            // Simon: this will geneate an error
            //efCrew = crew.ToCrew();
            crew.Update(efCrew);
            Context.Entry(efCrew).State = EntityState.Modified;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewExists(id)) 
                {
                    Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(PutCrew), id);
                    return NotFound(); 
                }
                else { throw; }
            }
            return NoContent();
        }

        // Delete : api/Crews/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<CrewHacked>> DeleteCrew(int id)
        {
            if (Context.Crews is null)
            {
                Logger.LogError(@"{func} called with {id} but there is no {table} table", nameof(DeleteCrew), id, nameof(Context.Crews));
                return NotFound();
            }
            var crew = await Context.Crews.FindAsync(id);
            if (crew is null)
            {
                Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(DeleteCrew), id);
                return NotFound();
            }
            Context.Crews.Remove(crew);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        private bool CrewExists(long id)
        {
            return (Context.Crews?.Any(crew => crew.CrewId == id)).GetValueOrDefault();
        }

    }
}
