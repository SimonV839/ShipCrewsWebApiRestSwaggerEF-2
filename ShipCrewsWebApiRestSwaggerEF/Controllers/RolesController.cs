using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ShipCrewsContext _context;

        public RolesController(ShipCrewsContext context)
        {
            _context = context;
        }

        // Get : api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            return await _context.Roles.ToListAsync();
        }

        // Get : api/Roles/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            if (_context.Roles is null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FindAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            return role;
        }
    }
}
