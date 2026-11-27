using BCrypt.Net;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rever.Dtos;
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

        private string ObtenerBaseUrlBackend()
        {
            var origin = Request.Headers.Origin.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(origin) && !origin.Contains(":5500"))
            {
                return origin.TrimEnd('/');
            }

            var codespaceName = Environment.GetEnvironmentVariable("CODESPACE_NAME");
            if (!string.IsNullOrWhiteSpace(codespaceName))
            {
                var domain = Environment.GetEnvironmentVariable("GITHUB_CODESPACES_PORT_FORWARDING_DOMAIN") ?? "app.github.dev";
                return $"https://{codespaceName}-7015.{domain}";
            }

            var forwardedHost = Request.Headers["X-Forwarded-Host"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwardedHost))
            {
                var forwardedProto = Request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? "https";
                return $"{forwardedProto}://{forwardedHost}";
            }

            return $"{Request.Scheme}://{Request.Host}";
        }

        private string ObtenerBaseUrlFrontend()
        {
            var origin = Request.Headers.Origin.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(origin) && origin.Contains(":5500"))
            {
                return $"{origin.TrimEnd('/')}/Frontend/html";
            }

            return $"{ObtenerBaseUrlBackend()}/html";
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

            if (!usuario.Confirmado)
            {
                return Unauthorized("Debes confirmar tu cuenta desde el correo que te enviamos antes de iniciar sesión.");
            }

            // Configurar Claims 
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
        public async Task<IActionResult> Register([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null || string.IsNullOrWhiteSpace(nuevoUsuario.Correo) || string.IsNullOrWhiteSpace(nuevoUsuario.Contraseña))
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            var existente = await _usuarioRepository.GetByEmailWithRolAsync(nuevoUsuario.Correo);
            if (existente != null)
            {
                return BadRequest("El correo ya se encuentra registrado.");
            }

            // Solo se permite auto-registrar comprador (2) o vendedor (3); rol 1 (admin) solo lo crea otro admin
            bool esAdministrador = User.EsAdministrador();
            if (!esAdministrador && (nuevoUsuario.IdRol != 2 && nuevoUsuario.IdRol != 3))
            {
                return BadRequest("Solo se permite la creación de usuarios con rol 2 o 3.");
            }

            string confirmationToken = Guid.NewGuid().ToString();

            nuevoUsuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Contraseña);
            nuevoUsuario.Confirmado = false;
            nuevoUsuario.TokenConfirmacion = confirmationToken;

            var guardado = await _usuarioRepository.PostUsuario(nuevoUsuario);
            if (!guardado)
            {
                return StatusCode(500, "Error interno al crear el usuario.");
            }

            string confirmationLink = $"{ObtenerBaseUrlBackend()}/api/Auth/ConfirmEmail?email={Uri.EscapeDataString(nuevoUsuario.Correo)}&token={Uri.EscapeDataString(confirmationToken)}";

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
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var usuario = await _usuarioRepository.GetByEmailWithRolAsync(email);

            if (usuario == null || usuario.TokenConfirmacion != token)
            {
                return Redirect("/html/login.html?error=invalid_token");
            }

            usuario.Confirmado = true;
            usuario.TokenConfirmacion = null;
            await _usuarioRepository.PutUsuario(usuario);

            return Redirect("/html/login.html?confirmed=true");
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Correo))
            {
                return BadRequest("El correo es obligatorio.");
            }

            var usuario = await _usuarioRepository.GetByEmailWithRolAsync(dto.Correo);
            if (usuario == null)
            {
                return Ok(new { message = "Si el correo está registrado, recibirás un enlace de recuperación." });
            }

            if (!usuario.Confirmado)
            {
                return BadRequest("Tu cuenta aún no ha sido activada. Por favor, revisa tu correo y confirma tu cuenta antes de solicitar un cambio de contraseña.");
            }

            string resetToken = Guid.NewGuid().ToString();
            usuario.TokenConfirmacion = resetToken;

            var actualizado = await _usuarioRepository.PutUsuario(usuario);
            if (!actualizado)
            {
                return StatusCode(500, "Error interno al procesar la solicitud.");
            }

            string resetLink = $"{ObtenerBaseUrlFrontend()}/recuperar_contrasena.html?email={Uri.EscapeDataString(usuario.Correo)}&token={Uri.EscapeDataString(resetToken)}";

            string emailBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px;'>
                    <h2>Recuperación de Contraseña - Rever</h2>
                    <p>Hola <b>{usuario.Nombre}</b>, recibimos una solicitud para restablecer tu contraseña.</p>
                    <p>Haz clic en el siguiente botón para crear una nueva contraseña:</p>
                    <a href='{resetLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; font-weight: bold;'>Restablecer mi Contraseña</a>
                    <br/><br/>
                    <p><small>Si no solicitaste este cambio, puedes ignorar este mensaje.</small></p>
                </div>
            ";

            await _emailService.SendEmailAsync(usuario.Correo, "Recuperación de contraseña - Rever", emailBody);

            return Ok(new { message = "Si el correo está registrado, recibirás un enlace de recuperación." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Token) || string.IsNullOrWhiteSpace(dto.NuevaContraseña))
            {
                return BadRequest("Datos de recuperación inválidos.");
            }

            var usuario = await _usuarioRepository.GetByEmailWithRolAsync(dto.Correo);
            if (usuario == null)
            {
                return BadRequest("Solicitud de recuperación inválida.");
            }

            if (!usuario.Confirmado)
            {
                return BadRequest("Tu cuenta aún no ha sido activada. Por favor, confirma tu correo antes de cambiar la contraseña.");
            }

            if (string.IsNullOrEmpty(usuario.TokenConfirmacion) || usuario.TokenConfirmacion != dto.Token)
            {
                return BadRequest("El enlace de recuperación no es válido o ya fue utilizado.");
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(dto.NuevaContraseña);
            usuario.TokenConfirmacion = null;

            var actualizado = await _usuarioRepository.PutUsuario(usuario);
            if (!actualizado)
            {
                return StatusCode(500, "Error interno al actualizar la contraseña.");
            }

            return Ok(new { message = "Contraseña restablecida con éxito. Ya puedes iniciar sesión." });
        }
    }
}