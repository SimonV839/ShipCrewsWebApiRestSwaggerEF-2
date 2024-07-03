using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ShipCrewsContext _context;

        public PeopleController(ShipCrewsContext context)
        {
            _context = context;
        }

        // Get : api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            return await _context.People.ToListAsync();
            /* generates exception
            var shipCrewsContext = _context.People.Include(p => p.Role).Skip(7);
            var first = shipCrewsContext.First();
            return await shipCrewsContext.ToListAsync();
            */
        }

        // Get : api/People/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPeople(int id)
        {
            if (_context.People is null)
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person is null)
            {
                return NotFound();
            }
            return person;
        }

        // Post : api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPeople(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostPeople), new { id = person.PersonId }, person);
        }

        // Put : api/People/2
        [HttpPut]
        public async Task<ActionResult<Person>> PutPerson(int id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }
            _context.Entry(person).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        // Delete : api/People/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            if (_context.People is null)
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person is null)
            {
                return NotFound();
            }
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool PersonExists(long id)
        {
            return (_context.People?.Any(person => person.PersonId == id)).GetValueOrDefault();
        }
        /*
        // GET: People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Index()
        {
            var shipCrewsContext = _context.People.Include(p => p.Role);
            return await shipCrewsContext.ToListAsync();
        }

        // GET: People/Details/5
        public async Task<ActionResult<Person>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,RoleId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", person.RoleId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", person.RoleId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,RoleId")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", person.RoleId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
        */
    }
    }
