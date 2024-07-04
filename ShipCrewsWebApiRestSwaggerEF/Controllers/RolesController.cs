using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
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
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            if (Context.Roles == null)
            {
                Logger.LogError("{func} called but there is no {table} table", nameof(GetRole), nameof(Context.Roles));
                return NotFound();
            }
            return await Context.Roles.ToListAsync();
        }

        // Get : api/Roles/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            if (Context.Roles is null)
            {
                return NotFound();
            }
            var role = await Context.Roles.FindAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            return role;
        }
    }
}
