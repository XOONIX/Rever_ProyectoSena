using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories;
using rever.Repositories.Interfaces;
using System;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoRepository _contactorepository;

        public ContactoController(IContactoRepository repository)
        {
            _contactorepository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarContacto()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                var response = await _contactorepository.GetContacto();
                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron contactos registrados.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerContacto(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID proporcionado no es válido.");
                }

                var response = await _contactorepository.GetContactoById(id);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró un contacto con ID {id}.");
                }

                // 1. Obtener ID del usuario desde el token de forma segura
                var idUsuarioToken = User.ObtenerIdUsuario();

                if (!idUsuarioToken.HasValue)
                {
                    return StatusCode(401, "401: No se pudo verificar la identidad desde el token.");
                }

                // 2. Comprobar permisos
                var esAdmin = User.IsInRole("administrador") || User.IsInRole("Admin");
                var esParteDelMensaje = response.IdComprador == idUsuarioToken.Value || response.IdVendedor == idUsuarioToken.Value;

                if (!esParteDelMensaje && !esAdmin)
                {
                    return StatusCode(403, "403: No puedes ver un mensaje que no te involucra.");
                }

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrearContacto([FromBody] Contacto contacto)
        {
            try
            {
                if (contacto == null)
                {
                    return StatusCode(400, "400: Los datos del contacto no pueden ser nulos.");
                }

                // 1. Obtener el ID del usuario desde el token de forma segura
                var idUsuarioToken = User.ObtenerIdUsuario();

                if (!idUsuarioToken.HasValue)
                {
                    return StatusCode(401, "401: No se pudo verificar la identidad desde el token.");
                }

                // 2. El remitente siempre es el usuario autenticado
                contacto.IdComprador = idUsuarioToken.Value;
                contacto.Fecha = DateTime.UtcNow;

                var response = await _contactorepository.PostContacto(contacto);
                if (response == null)
                {
                    return StatusCode(500, "500: Error interno al intentar crear el recurso.");
                }

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarContacto([FromBody] Contacto contacto)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (contacto == null || contacto.IdContacto <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _contactorepository.GetContactoById(contacto.IdContacto);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. El contacto con ID {contacto.IdContacto} no existe.");
                }

                exist.IdComprador = contacto.IdComprador;
                exist.IdVendedor = contacto.IdVendedor;
                exist.IdInmueble = contacto.IdInmueble;
                exist.Mensaje = contacto.Mensaje;
                exist.Fecha = contacto.Fecha;
                var response = await _contactorepository.PutContacto(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize (Roles ="administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID del contacto no es válido.");
                }

                var exist = await _contactorepository.GetContactoById(id);

                if (exist == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede eliminar. El contacto con ID {id} no existe."
                    );
                }

                var response = await _contactorepository.DeleteContacto(exist);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }
    }
}
