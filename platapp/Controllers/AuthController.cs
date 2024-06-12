using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using platapp.Domain;

namespace platapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PContext _context;

        public AuthController(PContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser request)
        {
            var user = _context.Utilisateur.FirstOrDefault(u => u.username == request.username && u.Passwd == request.Passwd);
            if (user == null)
            {
                return Unauthorized();
            }

            // Retourner un token JWT ou un autre mécanisme de session
            return Ok(new { Token = "dummy-token", user });
        }
    }
}