using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace platapp.ServicesAPI
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly PContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IOptions<JwtConfig> jwtConfig, PContext context, IHttpContextAccessor httpContextAccessor)
        {
            _jwtConfig = jwtConfig.Value;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Utilisateur.FirstOrDefaultAsync(u => u.username == username && u.Passwd == password);
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Utilisateur> GetAuthenticatedUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return null;
            }

            var userId = int.Parse(userIdClaim.Value);
            return await _context.Utilisateur.FindAsync(userId);
        }
    }
}
