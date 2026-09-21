using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rever.Dtos;
using rever.Models;
using rever.Repositories;
using rever.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InmuebleController : ControllerBase
    {
        private readonly IInmuebleRepository _inmueblerepository;

        public InmuebleController(IInmuebleRepository repository)
        {
            _inmueblerepository = repository;
        }

        [HttpGet("listar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarInmueble()
        {
            try
            {
                var response = await _inmueblerepository.GetInmueble();
                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron inmuebles registrados.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInmueble(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID proporcionado no es válido.");
                }

                var response = await _inmueblerepository.GetInmuebleById(id);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró el inmueble con ID {id}.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "vendedor,administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (inmueble == null)
                {
                    return StatusCode(400, "400: Los datos del inmueble no pueden ser nulos.");
                }

                // 1. Obtener el ID del usuario autenticado desde el token de forma segura
                var idUsuarioToken = ObtenerIdUsuarioToken();

                if (!idUsuarioToken.HasValue)
                {
                    return StatusCode(401, "401: No se pudo verificar la identidad desde el token.");
                }

                // 2. Asignar de forma segura el valor entero extraído (.Value)
                inmueble.IdUsuario = idUsuarioToken.Value;

                // 3. Crear el inmueble a través del repositorio
                var response = await _inmueblerepository.PostInmueble(inmueble);

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
        [Authorize(Roles = "vendedor,administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (inmueble == null || inmueble.IdInmueble <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _inmueblerepository.GetInmuebleById(inmueble.IdInmueble);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. El inmueble con ID {inmueble.IdInmueble} no existe.");
                }

                if (!EsDuenoOAdmin(exist.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes modificar un inmueble que no te pertenece.");
                }

                exist.Titulo = inmueble.Titulo;
                exist.Descripcion = inmueble.Descripcion;
                exist.Precio = inmueble.Precio;
                exist.IdTipo = inmueble.IdTipo;
                exist.Direccion = inmueble.Direccion;
                exist.IdBarrio = inmueble.IdBarrio;
                exist.Habitaciones = inmueble.Habitaciones;
                exist.Baños = inmueble.Baños;
                exist.MetrosCuadrados = inmueble.MetrosCuadrados;
                exist.Estrato = inmueble.Estrato;
                exist.Latitud = inmueble.Latitud;
                exist.Longitud = inmueble.Longitud;
                exist.IdEstado = inmueble.IdEstado;
                // IdUsuario NO se actualiza aquí — el dueño de un inmueble no debería
                // poder "regalárselo" a otro usuario cambiando este campo desde el formulario.

                var response = await _inmueblerepository.PutInmueble(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "vendedor,administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarInmueble(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o el ID no son válidos.");
                }

                var exist = await _inmueblerepository.GetInmuebleById(id);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede eliminar. El inmueble con ID {id} no existe.");
                }

                if (!EsDuenoOAdmin(exist.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes eliminar un inmueble que no te pertenece.");
                }

                var response = await _inmueblerepository.DeleteInmueble(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var inmuebles = await _inmueblerepository.GetListadoAsync();
            return Ok(inmuebles);
        }

        private int? ObtenerIdUsuarioToken()
        {
            return User.ObtenerIdUsuario();
        }

        private bool EsDuenoOAdmin(int idDuenoDelRecurso)
        {
            var idUsuarioToken = ObtenerIdUsuarioToken();

            if (!idUsuarioToken.HasValue)
            {
                return false;
            }

            var esAdmin = User.IsInRole("administrador") || User.IsInRole("Admin");

            return idDuenoDelRecurso == idUsuarioToken.Value || esAdmin;
        }
    }
}