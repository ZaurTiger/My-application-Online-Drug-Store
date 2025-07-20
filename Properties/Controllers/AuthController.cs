using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api_v2.Properties.Data;
using Api_v2.Properties.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api_v2.Properties.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        public AuthController(AppDbContext context)
        {
            //инициализация по дефолту
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {   
            if (_context.Users.Any(u => u.Login == user.Login))
                return BadRequest("User with that login already exists");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created");
        }
        
        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
                return Unauthorized("Wrong login or password");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
