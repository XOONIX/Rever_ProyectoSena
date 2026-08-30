using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _Usuariorrepository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _Usuariorrepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarUsuario()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                var response = await _Usuariorrepository.GetUsuario();

                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron usuarios registrados.");
                }

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID proporcionado no es válido.");
                }

                var exist = await _Usuariorrepository.GetUsuarioById(id);

                if (exist == null)
                {
                    return StatusCode(404, $"404: No se encontró el usuario con ID {id}.");
                }

                return StatusCode(200, exist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500: Error interno del servidor. Detalle: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario Usuario)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (Usuario == null || string.IsNullOrWhiteSpace(Usuario.Contraseña))
                {
                    return StatusCode(400, "400: Los datos del usuario no pueden ser nulos.");
                }

                // Hashear la contraseña ANTES de guardar
                Usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(Usuario.Contraseña);

                var response = await _Usuariorrepository.PostUsuario(Usuario);

                if (response == null || response == false)
                {
                    return StatusCode(500, "500: Error interno al intentar crear el recurso.");
                }

                // No devolvemos la contraseña/hash en la respuesta
                Usuario.Contraseña = null;

                return StatusCode(200, Usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario Usuario)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (Usuario == null || Usuario.IdUsuario <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _Usuariorrepository.GetUsuarioById(Usuario.IdUsuario);

                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. El usuario con ID {Usuario.IdUsuario} no existe.");
                }

                exist.Nombre = Usuario.Nombre;
                exist.Correo = Usuario.Correo;
                exist.Telefono = Usuario.Telefono;
                exist.IdRol = Usuario.IdRol;

                if (!string.IsNullOrWhiteSpace(Usuario.Contraseña))
                {
                    exist.Contraseña = BCrypt.Net.BCrypt.HashPassword(Usuario.Contraseña);
                }

                var response = await _Usuariorrepository.PutUsuario(exist);   // <- corregido

                exist.Contraseña = null;   // <- también corregido: limpia "exist", que es lo que realmente tiene el hash

                return StatusCode(200, exist);   // <- devuelve "exist", no "Usuario"
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500: Error interno del servidor. Detalle: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID del usuario no es válido.");
                }

                var exist = await _Usuariorrepository.GetUsuarioById(id);

                if (exist == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede eliminar. El usuario con ID {id} no existe."
                    );
                }

                var response = await _Usuariorrepository.DeleteUsuario(exist);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }
    }
}
