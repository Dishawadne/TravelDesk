
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;
using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly DbContexts _context;
        public UserController(IUserRepository userRepository, DbContexts context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            var createdUser = _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            var updatedUser = _userRepository.UpdateUser(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var isDeleted = _userRepository.DeleteUser(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<User>>> GetManagers()
        {
            var managers = await _context.Users
                .Where(u => u.Role.RoleName == "Manager" && u.IsActive)
                .ToListAsync();

            return Ok(managers);
        }
    }
}
