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
    public class SessionAttendeesController : ControllerBase
    {
        private readonly TestDBContext _context;

        public SessionAttendeesController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/SessionAttendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionAttendees>>> GetSessionAttendees()
        {
            return await _context.SessionAttendees.ToListAsync();
        }

        // GET: api/SessionAttendees/5
        [HttpGet("{id}/{sesid}")]
        public async Task<ActionResult<SessionAttendees>> GetSessionAttendees(int id, int sesid)
        {
            var sessionAttendees = await _context.SessionAttendees.FindAsync(id, sesid);

            if (sessionAttendees == null)
            {
                return NotFound();
            }

            return sessionAttendees;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionAttendees>> GetSessionAttendees(int id)
        {
            var sessionAttendees = await _context.SessionAttendees.FindAsync(id);

            if (sessionAttendees == null)
            {
                return NotFound();
            }

            return sessionAttendees;
        }

        // PUT: api/SessionAttendees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionAttendees(int id, SessionAttendees sessionAttendees)
        {
            if (id != sessionAttendees.UserId)
            {
                return BadRequest();
            }

            _context.Entry(sessionAttendees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionAttendeesExists(id))
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

        // POST: api/SessionAttendees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SessionAttendees>> PostSessionAttendees(SessionAttendees sessionAttendees)
        {
            _context.SessionAttendees.Add(sessionAttendees);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SessionAttendeesExists(sessionAttendees.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSessionAttendees", new { id = sessionAttendees.UserId }, sessionAttendees);
        }

        // DELETE: api/SessionAttendees/5
        [HttpDelete("{id}/{sesid}")]
        public async Task<ActionResult<SessionAttendees>> DeleteSessionAttendees(int id, int sesid)
        {
            var sessionAttendees = await _context.SessionAttendees.FindAsync(id, sesid);
            if (sessionAttendees == null)
            {
                return NotFound();
            }

            _context.SessionAttendees.Remove(sessionAttendees);
            await _context.SaveChangesAsync();

            return sessionAttendees;
        }

        private bool SessionAttendeesExists(int id)
        {
            return _context.SessionAttendees.Any(e => e.UserId == id);
        }
    }
}
