using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    //Simon: this does not map to a db single item

    [Route("api/[controller]")]
    [ApiController]
    public class CrewMembersController : ControllerBase
    {
        private readonly ShipCrewsContext _context;

        public CrewMembersController(ShipCrewsContext context)
        {
            _context = context;
        }

        // Get : api/CrewMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewMembers>>> GetRole()
        {
            if (_context.CrewAssignments == null)
            {
                return NotFound();
            }

            var assignments = await _context.CrewAssignments.Select(x => new { Index = x.CrewId, Value = x.PersonId }).
                GroupBy(x => x.Index).
                ToListAsync();

            if (!assignments.Any())
            {
                return NotFound();
            }

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
            if (_context.CrewAssignments == null)
            {
                return NotFound();
            }

            var members = await _context.CrewAssignments.Where(c => c.CrewId == crewId).Select(i => i.PersonId ?? -1).ToListAsync();
            if (!members.Any())
            {
                return NotFound();
            }

            return new CrewMembers { CrewId = 1, Members = members };
        }

        // Put : api/CrewMembers/2
        [HttpPut]
        public async Task<ActionResult<CrewMembers>> PutCrewMembers(int crewId, ICollection<int> members)
        {
            var orderedMembers = members.Distinct().Order().ToList();

            var foundAssignments = await _context.CrewAssignments.Where(c => c.CrewId == crewId).ToListAsync();
            if (!foundAssignments.Any())
            {
                return NotFound();
            }

            var toDelete = foundAssignments.Where(a => !orderedMembers.Contains(a.PersonId ?? -1));
            foreach(var mem in toDelete)
            {
                _context.CrewAssignments.Remove(mem);
            }

            var foundPersonIds = foundAssignments.Select(s => s.PersonId).ToList();
            var notPresent = orderedMembers.Where(a => !foundPersonIds.Contains(a));
            foreach(var mem in notPresent)
            {
                await _context.CrewAssignments.AddAsync(new CrewAssignment { CrewId = crewId, PersonId = mem});
            }

            await _context.SaveChangesAsync();

            return new CrewMembers { CrewId = crewId, Members = orderedMembers };
        }

    }
}
