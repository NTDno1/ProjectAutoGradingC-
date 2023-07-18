using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectPrn231Context _context;
        private string secretKey;
        public UsersController(ProjectPrn231Context context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<User>> GetUsers(string name)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("{userName}/{passWord}")]
        public async Task<ActionResult<User>> GetUser(string userName, string passWord)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.PassWord == passWord);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{className}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(string className)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Where(u=>u.Class.Name == className).ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ProjectPrn231Context.Users'  is null.");  
          }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
