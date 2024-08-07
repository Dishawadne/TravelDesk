using Microsoft.AspNetCore.Mvc;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetDepartments()
        {
            var departments = Enum.GetNames(typeof(Department));
            return Ok(departments);
        }
    }
}
