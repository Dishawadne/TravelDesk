using Microsoft.AspNetCore.Mvc;
using TravelDesk.Context;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly DbContexts _context;
        public RoleController(DbContexts context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            var roles = _context.Roles
                .Where(role => role.RoleName != "admin") // Filter out admin role
                .Select(role => new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                })
                .ToList();

            return Ok(roles);
        }


    }
}
