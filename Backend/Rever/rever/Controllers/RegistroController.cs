using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using rever.Models;
using rever.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RegistroController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null || string.IsNullOrWhiteSpace(usuario.Contraseña) || string.IsNullOrWhiteSpace(usuario.Correo))
                {
                    return StatusCode(400, "400: Los datos del usuario no pueden ser nulos.");
                }

                // Lista blanca de roles permitidos para autoregistro
                // Ajusta estos IDs según tu tabla "roles" real
                const int ID_ROL_COMPRADOR = 2;
                const int ID_ROL_VENDEDOR = 3;

                if (usuario.IdRol != ID_ROL_COMPRADOR && usuario.IdRol != ID_ROL_VENDEDOR)
                {
                    return StatusCode(400, "400: El rol seleccionado no es válido para el registro.");
                }

                var existente = await _usuarioRepository.GetByEmailWithRolAsync(usuario.Correo);
                if (existente != null)
                {
                    return StatusCode(409, "409: Ya existe un usuario registrado con este correo.");
                }

                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

                var response = await _usuarioRepository.PostUsuario(usuario);

                if (response == null || response == false)
                {
                    return StatusCode(500, "500: Error interno al intentar crear el recurso.");
                }

                usuario.Contraseña = null;

                return StatusCode(200, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500: Error interno del servidor. {ex.Message}");
            }
        }
    }
}