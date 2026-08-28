using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rever.Models;
using rever.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

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
        if (login == null || string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contraseña))
        {
            return BadRequest("Invalid client request");
        }

        // 1. Buscar el usuario por Correo (incluyendo su rol) en la base de datos
        var usuario = await _usuarioRepository.GetByEmailWithRolAsync(login.Correo);

        if (usuario == null)
        {
            return Unauthorized("Credenciales inválidas");
        }

        // 2. Verificar la contraseña contra el hash almacenado (nunca texto plano)
        bool passwordValida = BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña);
        if (!passwordValida)
        {
            return Unauthorized("Credenciales inválidas");
        }

        // 3. Verificar que el rol sea Administrador
        if (usuario.Rol?.Nombre != "administrador")
        {
            return Forbid(); // 403: existe, pero no tiene el rol requerido
        }

        // 4. Generar el token solo si pasó las dos validaciones
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            },
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new { Token = tokenString });
    }
}