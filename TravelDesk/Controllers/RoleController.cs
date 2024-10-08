﻿using Microsoft.AspNetCore.Mvc;
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
                .Where(role => role.RoleName != "admin") 
                .Select(role => new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                })
                .ToList();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == id);
        }


    }
}
