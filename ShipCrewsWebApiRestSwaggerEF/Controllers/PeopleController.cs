using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipCrewsWebApiRestSwaggerEF.HackedModels;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ShipCrewsControllerBase<PeopleController>
    {
        public PeopleController(ILogger<PeopleController> logger, ShipCrewsContext context)
            : base(logger, context)
        {
        }

        // Get : api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonHacked>>> GetPeople()
        {
            if (Context.People == null)
            {
                Logger.LogError(@"{func} called but there is no {table} table", nameof(GetPeople), nameof(Context.People));
                return NotFound();
            }
            var efPeople = await Context.People.ToListAsync();
            return efPeople.Select(ef => new PersonHacked(ef)).ToList();
        }

        // Get : api/People/2
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonHacked>> GetPeople(int id)
        {
            if (Context.People is null)
            {
                Logger.LogError(@"{func} called with {id} but there is no {table} table", nameof(GetPeople), id, nameof(Context.People));
                return NotFound();
            }
            var person = await Context.People.FindAsync(id);
            if (person is null)
            {
                Logger.LogInformation(@"{func} called with {id} but this is not present in {table} table", nameof(GetPeople), id, nameof(Context.People));
                return NotFound();
            }
            return new PersonHacked(person);
        }

        // Post : api/People
        [HttpPost]
        // Simon: the auto generated code for this as a referenced service
        // will check for 200 without this
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PersonHacked>> PostPeople(PersonHacked person)
        {
            var efPerson = person.ToPerson();
            Context.People.Add(efPerson);
            await Context.SaveChangesAsync();
            Logger.LogInformation("{PostPeople} success. Returning CreatedAtAction with id: {efPerson.PersonId}, person: {efPerson}", 
                nameof(PostPeople), efPerson.PersonId, efPerson);
            return CreatedAtAction(nameof(PostPeople), new { id = efPerson.PersonId }, new PersonHacked(efPerson));
        }

        // Put : api/People/2
        [HttpPut]
        public async Task<ActionResult<PersonHacked>> PutPerson(int id, PersonHacked person)
        {
            if (id != person.PersonId)
            {
                Logger.LogError(@"{func} called with {id} but this does match the value supplied in {person}", nameof(PutPerson), id, person);
                return BadRequest();
            }
            var efPerson = await Context.People.FindAsync(id);
            if (efPerson is null)
            {
                Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(PutPerson), id);
                return NotFound();
            }
            person.Update(efPerson);
            Context.Entry(efPerson).State = EntityState.Modified;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id)) 
                {
                    Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(PutPerson), id);
                    return NotFound(); 
                }
                else { throw; }
            }
            return NoContent();
        }

        // Delete : api/People/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            if (Context.People is null)
            {
                Logger.LogError(@"{func} called with {id} but there is no {table} table", nameof(DeletePerson), id, nameof(Context.People));
                return NotFound();
            }
            var person = await Context.People.FindAsync(id);
            if (person is null)
            {
                Logger.LogWarning(@"{func} called with {id} but an item with this id does not exist", nameof(DeletePerson), id);
                return NotFound();
            }
            Context.People.Remove(person);
            await Context.SaveChangesAsync();
            return NoContent();
        }
        private bool PersonExists(long id)
        {
            return (Context.People?.Any(person => person.PersonId == id)).GetValueOrDefault();
        }
        /*
        // GET: People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Index()
        {
            var shipCrewsContext = Context.People.Include(p => p.Role);
            return await shipCrewsContext.ToListAsync();
        }

        // GET: People/Details/5
        public async Task<ActionResult<Person>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await Context.People
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
            ViewData["RoleId"] = new SelectList(Context.Roles, "RoleId", "RoleId");
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
                Context.Add(person);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(Context.Roles, "RoleId", "RoleId", person.RoleId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await Context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(Context.Roles, "RoleId", "RoleId", person.RoleId);
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
                    Context.Update(person);
                    await Context.SaveChangesAsync();
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
            ViewData["RoleId"] = new SelectList(Context.Roles, "RoleId", "RoleId", person.RoleId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await Context.People
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
            var person = await Context.People.FindAsync(id);
            if (person != null)
            {
                Context.People.Remove(person);
            }

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return Context.People.Any(e => e.PersonId == id);
        }
        */
    }
    }
