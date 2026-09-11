using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories.Interfaces;
using System;
using System.Security.Claims;
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (usuario == null || string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Contraseña))
                {
                    return StatusCode(400, "400: Los datos del usuario o la contraseña no pueden estar vacíos.");
                }

                // 1. Validar que el correo no esté registrado previamente
                var usuarioExistente = await _Usuariorrepository.GetByEmailWithRolAsync(usuario.Correo);
                if (usuarioExistente != null)
                {
                    return StatusCode(400, "400: El correo ya se encuentra registrado.");
                }

                // 2. Obtener el rol del usuario que realiza la petición desde los Claims
                var rolUsuarioAutenticado = User.FindFirst(ClaimTypes.Role)?.Value;

                // 3. Validar restricción de creación de roles
                // Si no es Administrador (Rol 1), solo puede registrar usuarios con Rol 2 o 3
                bool esAdministrador = rolUsuarioAutenticado == "1" || rolUsuarioAutenticado == "Administrador";

                if (!esAdministrador && usuario.IdRol == 1)
                {
                    return StatusCode(403, "403: No tienes permisos para crear usuarios con rol 1.");
                }

                // Si tampoco especifica un rol permitido (solo 2 o 3 para usuarios normales)
                if (!esAdministrador && (usuario.IdRol != 2 && usuario.IdRol != 3))
                {
                    return StatusCode(400, "400: Solo se permite la creación de usuarios con rol 2 o 3.");
                }

                // 4. Hashear la contraseña ANTES de guardar
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

                var response = await _Usuariorrepository.PostUsuario(usuario);

                if (!response)
                {
                    return StatusCode(500, "500: Error interno al intentar crear el recurso.");
                }

                usuario.Contraseña = null;

                return StatusCode(200, usuario);
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

                var response = await _Usuariorrepository.PutUsuario(exist);

                exist.Contraseña = null;  

                return StatusCode(200, exist);  
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
