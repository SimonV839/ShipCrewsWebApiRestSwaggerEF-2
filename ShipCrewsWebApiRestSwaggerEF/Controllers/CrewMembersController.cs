using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    //Simon: this does not map to a db single item

    [Route("api/[controller]")]
    [ApiController]
    public class CrewMembersController : ShipCrewsControllerBase<CrewMembersController>
    {
        public CrewMembersController(ILogger<CrewMembersController> logger, ShipCrewsContext context)
            : base(logger, context)
        {
        }

        // Get : api/CrewMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewMembers>>> GetRole()
        {
            if (Context.CrewAssignments == null)
            {
                Logger.LogError("{func} called but there is no {table} table", nameof(GetRole), nameof(Context.CrewAssignments));
                return NotFound();
            }

            var assignments = await Context.CrewAssignments.Select(x => new { Index = x.CrewId, Value = x.PersonId }).
                GroupBy(x => x.Index).
                ToListAsync();

            var crews = new List<CrewMembers>();

            foreach (var group in assignments)
            {
                crews.Add(new CrewMembers { CrewId = group.First().Index ?? -1, Members = group.Select(i => i.Value ?? -1).ToList()});
            }

            return crews;
        }

        // Get : api/CrewMembers
        [HttpGet("{crewId}")]
        public async Task<ActionResult<CrewMembers>> GetCrewMembers(int crewId)
        {
            if (Context.CrewAssignments == null)
            {
                Logger.LogError("{func} called but there is no {table} table", nameof(GetCrewMembers), nameof(Context.CrewAssignments));
                return NotFound();
            }

            var members = await Context.CrewAssignments.Where(c => c.CrewId == crewId).Select(i => i.PersonId ?? -1).ToListAsync();
            if (!members.Any())
            {
                Logger.LogInformation("{func} called with {id} but this is not present in {table} table", nameof(GetCrewMembers), crewId, nameof(Context.CrewAssignments));
                return NotFound();
            }

            return new CrewMembers { CrewId = 1, Members = members };
        }

        // Put : api/CrewMembers/2
        [HttpPut]
        public async Task<ActionResult<CrewMembers>> PutCrewMembers(int crewId, ICollection<int> members)
        {
            var orderedMembers = members.Distinct().Order().ToList();

            var foundAssignments = await Context.CrewAssignments.Where(c => c.CrewId == crewId).ToListAsync();
            if (!foundAssignments.Any())
            {
                Logger.LogInformation("{func} called with {crewId} but this is not present in {table} table", nameof(PutCrewMembers), crewId, nameof(Context.CrewAssignments));
                return NotFound();
            }

            var toDelete = foundAssignments.Where(a => !orderedMembers.Contains(a.PersonId ?? -1));
            foreach(var mem in toDelete)
            {
                Context.CrewAssignments.Remove(mem);
            }

            var foundPersonIds = foundAssignments.Select(s => s.PersonId).ToList();
            var notPresent = orderedMembers.Where(a => !foundPersonIds.Contains(a));
            foreach(var mem in notPresent)
            {
                await Context.CrewAssignments.AddAsync(new CrewAssignment { CrewId = crewId, PersonId = mem});
            }

            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
