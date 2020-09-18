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
    public class SessionsController : ControllerBase
    {
        private readonly TestDBContext _context;

        public SessionsController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sessions>>> GetSessions()
        {
            return await _context.Sessions.ToListAsync();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sessions>> GetSessions(int id)
        {
            var sessions = _context.Sessions
                                        .Include(ses => ses.SessionAttendees)
                                        .Include(ses => ses.SessionType)
                                        .Where(ses => ses.Id == id)
                                        .FirstOrDefault();

            if (sessions == null)
            {
                return NotFound();
            }

            return sessions;
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessions(int id, Sessions sessions)
        {
            if (id != sessions.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionsExists(id))
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

        // POST: api/Sessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sessions>> PostSessions(Sessions sessions)
        {
            _context.Sessions.Add(sessions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessions", new { id = sessions.Id }, sessions);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sessions>> DeleteSessions(int id)
        {
            var sessionAttendees = await _context.SessionAttendees.Where(sa => sa.SessionId == id).ToListAsync();
            var sessions = await _context.Sessions.FindAsync(id);
            if (sessions == null)
            {
                return NotFound();
            }

            for (var i = 0; i < sessionAttendees.Count; i++) 
            {
                _context.SessionAttendees.Remove(sessionAttendees[i]);
                await _context.SaveChangesAsync();
            }

            _context.Sessions.Remove(sessions);
            await _context.SaveChangesAsync();

            return sessions;
        }

        private bool SessionsExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
