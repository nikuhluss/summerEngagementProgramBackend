using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstPlannerAPI.Models;
using System.Web.Http.Cors;

namespace DBFirstPlannerAPI.Controllers
{
    //[EnableCors(origins: "http://localhost:3001", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TestDBContext _context;

        public UsersController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("getUsers/{id}")]
        public ActionResult<Users> GetUsers(int id)
        {
            var users = _context.Users
                                    .Include(ses => ses.SessionAttendees)
                                    .ThenInclude(s => s.Session)
                                    .Where(usr => usr.Id == id)
                                    .FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/Users/5
        [HttpGet("getUsersByEmail/{email}")]
        public ActionResult<Users> GetUsersByEmail(string email)
        {
            var users = _context.Users
                                    .Where(usr => usr.Email == email)
                                    .FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet("getUsersAndDetails/{id}")]
        public ActionResult<Users> GetUsersAndDetails(int id)
        {
            var users = _context.Users
                                    .Include(usr => usr.UserDetails)
                                    .Where(usr => usr.Id == id)
                                    .FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet("getUserAndSessions/{id}")]
        public ActionResult<Users> GetUserAndSessions(int id)
        {
            var users = _context.Users
                                    .Include(usr => usr.SessionAttendees)
                                    .Include(usr => usr.Sessions)
                                    .Where(usr => usr.Id == id)
                                    .FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            //have to do password hash and stuff

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            try
            {
                _context.Users.Add(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.Write(ex);
            }
            

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        [HttpPost("verifyUser")]
        public ActionResult<String> PostVerifyUsers(Users users)
        {

            var user = _context.Users
                                    .Where(usr => usr.Email == users.Email && usr.PasswordSat == users.PasswordSat)
                                    .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return "Success";
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
