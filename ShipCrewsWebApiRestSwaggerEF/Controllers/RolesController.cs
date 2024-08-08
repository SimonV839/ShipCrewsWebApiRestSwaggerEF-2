using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimonV839.ShipCrewsWebApiRestSwaggerEF.HackedModels;
using SimonV839.ShipCrewsWebApiRestSwaggerEF.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimonV839.ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ShipCrewsControllerBase<RolesController>
    {
        public RolesController(ILogger<RolesController> logger, ShipCrewsContext context)
            : base(logger, context)
        {
        }

        // Get : api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleHacked>>> GetRole()
        {
            if (Context.Roles == null)
            {
                Logger.LogError(@"{func} called but there is no {table} table", nameof(GetRole), nameof(Context.Roles));
                return NotFound();
            }

            var efRoles = await Context.Roles.ToListAsync();
            return efRoles.Select(ef => new RoleHacked(ef)).ToList();
        }

        // Get : api/Roles/2
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleHacked>> GetRole(int id)
        {
            if (Context.Roles is null)
            {
                return NotFound();
            }
            var role = await Context.Roles.FindAsync(id);
            if (role is null)
            {
                Logger.LogInformation(@"{func} called with {id} but this is not present in {table} table", nameof(GetRole), id, nameof(Context.Roles));
                return NotFound();
            }
            return new RoleHacked(role);
        }
    }
}
