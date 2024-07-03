using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
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
        [HttpGet("{crewId}")]
        public async Task<ActionResult<CrewMembers>> GetCrewMembers(int crewId)
        {
            if (_context.Crews == null)
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
            var foundAssignments = await _context.CrewAssignments.Where(c => c.CrewId == crewId).ToListAsync();
            if (!foundAssignments.Any())
            {
                return NotFound();
            }

            var toDelete = foundAssignments.Where(a => !members.Contains(a.PersonId ?? -1));
            foreach(var mem in toDelete)
            {
                _context.CrewAssignments.Remove(mem);
            }
            foreach(var mem in members)
            {
                await _context.CrewAssignments.AddAsync(new CrewAssignment { CrewId = crewId, PersonId = mem});
            }

            await _context.SaveChangesAsync();

            return new CrewMembers { CrewId = crewId, Members = members };
        }

    }
}
