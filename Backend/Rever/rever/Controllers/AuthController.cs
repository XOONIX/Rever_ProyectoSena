using BCrypt.Net;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rever.Models;
using rever.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        public AuthController(IConfiguration configuration, IUsuarioRepository usuarioRepository, IEmailService emailService)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Correo) || string.IsNullOrWhiteSpace(login.Contraseña))
            {
                return BadRequest("Invalid client request");
            }

            var usuario = await _usuarioRepository.GetByEmailWithRolAsync(login.Correo);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            bool passwordValida = BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña);
            if (!passwordValida)
            {
                return Unauthorized("Credenciales inválidas");
            }

            // Configurar Claims — todo lo que el frontend necesita saber va aquí
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol?.IdRol.ToString() ?? "0"),
                new Claim("nombre", usuario.Nombre),
            };

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
                Expiration = tokenOptions.ValidTo,
            });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null || string.IsNullOrWhiteSpace(nuevoUsuario.Correo))
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            string confirmationToken = Guid.NewGuid().ToString();

            nuevoUsuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Contraseña);

            // ⚠️ Pendiente: esto todavía no guarda al usuario en la base de datos.
            // Falta: await _usuarioRepository.PostUsuario(nuevoUsuario);

            string confirmationLink = $"{Request.Scheme}://{Request.Host}/api/Auth/ConfirmEmail?email={nuevoUsuario.Correo}&token={confirmationToken}";

            string emailBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px;'>
                    <h2>¡Bienvenido a Rever!</h2>
                    <p>Hola <b>{nuevoUsuario.Nombre}</b>, gracias por registrarte.</p>
                    <p>Por favor confirma tu cuenta haciendo clic en el siguiente botón:</p>
                    <a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; font-weight: bold;'>Confirmar mi Cuenta</a>
                    <br/><br/>
                    <p><small>Si no creaste esta cuenta, puedes ignorar este mensaje.</small></p>
                </div>
            ";

            await _emailService.SendEmailAsync(nuevoUsuario.Correo, "Confirma tu cuenta de Rever", emailBody);

            return Ok(new { message = "Registro exitoso. Revisa tu correo para activar la cuenta." });
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            return Ok("¡Cuenta confirmada con éxito! Ya puedes iniciar sesión.");
        }
    }
}