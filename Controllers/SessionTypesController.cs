using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstPlannerAPI.Models;

namespace DBFirstPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionTypesController : ControllerBase
    {
        private readonly TestDBContext _context;

        public SessionTypesController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/SessionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionTypes>>> GetSessionTypes()
        {
            return await _context.SessionTypes.ToListAsync();
        }

        // GET: api/SessionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionTypes>> GetSessionTypes(int id)
        {
            var sessionTypes = await _context.SessionTypes.FindAsync(id);

            if (sessionTypes == null)
            {
                return NotFound();
            }

            return sessionTypes;
        }

        // PUT: api/SessionTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionTypes(int id, SessionTypes sessionTypes)
        {
            if (id != sessionTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessionTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionTypesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SessionTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SessionTypes>> PostSessionTypes(SessionTypes sessionTypes)
        {
            _context.SessionTypes.Add(sessionTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessionTypes", new { id = sessionTypes.Id }, sessionTypes);
        }

        // DELETE: api/SessionTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionTypes>> DeleteSessionTypes(int id)
        {
            var sessionTypes = await _context.SessionTypes.FindAsync(id);
            if (sessionTypes == null)
            {
                return NotFound();
            }

            _context.SessionTypes.Remove(sessionTypes);
            await _context.SaveChangesAsync();

            return sessionTypes;
        }

        private bool SessionTypesExists(int id)
        {
            return _context.SessionTypes.Any(e => e.Id == id);
        }
    }
}
