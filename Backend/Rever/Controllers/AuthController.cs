using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rever.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("usuario")]
        public IActionResult usuario([FromBody] Usuario usuario)
        {
            if (usuario == null ||
                string.IsNullOrEmpty(usuario.Correo) ||
                string.IsNullOrEmpty(usuario.Contraseña))
            {
                return BadRequest("Invalid client request");
            }


            var configuredUsername = _configuration["Auth:Username"];
            var configuredPassword = _configuration["Auth:Password"];


            if (usuario.Correo == configuredUsername &&
                usuario.Contraseña == configuredPassword)
            {

                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );


                var signinCredentials = new SigningCredentials(
                    secretKey,
                    SecurityAlgorithms.HmacSha256
                );


                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Correo),
                        new Claim(ClaimTypes.Role, "Admin")
                    },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler()
                    .WriteToken(tokenOptions);

                return Ok(new
                {
                    Token = tokenString
                });
            }

            return Unauthorized();
        }
    }
}