using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rever.Models;
using rever.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Correo) || string.IsNullOrWhiteSpace(login.Contraseña))
            {
                return BadRequest("Invalid client request");
            }

            // 1. Buscar el usuario por Correo
            var usuario = await _usuarioRepository.GetByEmailWithRolAsync(login.Correo);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            // 2. Verificar la contraseña usando BCrypt
            bool passwordValida = BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña);
            if (!passwordValida)
            {
                return Unauthorized("Credenciales inválidas");
            }

            // 3. Configurar Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "User")
            };

            // 4. Generar Token JWT
            var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured."));
            var secretKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new 
            { 
                Token = tokenString,
                Expiration = tokenOptions.ValidTo
            });
        }
    }
}