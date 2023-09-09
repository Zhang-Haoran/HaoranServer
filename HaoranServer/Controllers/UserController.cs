using Microsoft.AspNetCore.Mvc;
using HaoranServer.Dto.UserDto;
using HaoranServer.Models;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _userService.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, UserPutDto userPutDto)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            await _userService.UpdateUser(userId, userPutDto);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserPostDto userPostDto)
        {
            var user = await _userService.CreateUser(userPostDto);
            return CreatedAtAction("GetUser", new { userId = user.UserId }, user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            await _userService.SoftDeleteUser(userId);
            return NoContent();
        }
    }
}
