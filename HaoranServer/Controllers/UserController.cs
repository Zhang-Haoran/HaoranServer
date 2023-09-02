using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.UserDto;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getuser()
        {
          if (_context.user == null)
          {
              return NotFound();
          }
            return await _context.user.Where(u => !u.IsDeleted).Include(r => r.Review).ToListAsync(); // Where(u => !u.IsDeleted) 避免显示 软删除的 用户
        }

        // GET: api/User/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
          if (_context.user == null)
          {
              return NotFound();
          }
            var user = await _context.user.Where(u => !u.IsDeleted).Include(r => r.Review).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, UserPutDto userPutDto)
        {
            if (userId != userPutDto.UserId)
            {
                return BadRequest();
            }
            var user = await _context.user.FindAsync(userId);
            _context.Entry(user).State = EntityState.Modified;
            user.FirstName = userPutDto.FirstName;
            user.LastName = userPutDto.LastName;
            user.DateOfBirth = userPutDto.DateOfBirth;
            user.Role = userPutDto.Role;
            user.Password = userPutDto.Password;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserPostDto userPostDto)
        {
          if (_context.user == null)
          {
              return Problem("Entity set 'UserContext.user'  is null.");
          }
            User user = new User();
            user.FirstName = userPostDto.FirstName;
            user.LastName = userPostDto.LastName;
            user.DateOfBirth = userPostDto.DateOfBirth;
            user.Role = userPostDto.Role;
            user.Password = userPostDto.Password;

            _context.user.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { userId = user.UserId }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (_context.user == null)
            {
                return NotFound();
            }
            var user = await _context.user.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int userId)
        {
            return (_context.user?.Any(e => e.UserId == userId)).GetValueOrDefault();
        }
    }
}
