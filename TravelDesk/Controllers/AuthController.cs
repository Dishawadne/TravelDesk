using Microsoft.AspNetCore.Mvc;


using TravelDesk.IRepository;
using TravelDesk.ViewModel;

namespace TravelDesk.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _authRepository.Authenticate(loginModel.Email, loginModel.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { message = "Login successful", user });
        }

    }
}
