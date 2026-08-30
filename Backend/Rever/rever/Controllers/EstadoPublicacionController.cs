using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories;
using rever.Repositories.Interfaces;
using System.Diagnostics.Contracts;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstadoPublicacionController : ControllerBase
    {
        private readonly IEstadoPublicacionRepository _estadopublicacionrepository;
        public EstadoPublicacionController(IEstadoPublicacionRepository repository)
        {
            _estadopublicacionrepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarEstadoPublicacion()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                var response = await _estadopublicacionrepository.GetEstadoPublicacion();
                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron estados de publicación registrados.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerEstadoPublicacion(int id)
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

                var response = await _estadopublicacionrepository.GetEstadoPublicacionById(id);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró el estado de publicación con ID {id}.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearEstadoPublicacion([FromBody] EstadoPublicacion estadopublicacion)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (estadopublicacion == null)
                {
                    return StatusCode(400, "400: Los datos del estado de publicación no pueden ser nulos.");
                }

                var response = await _estadopublicacionrepository.PostEstadoPublicacion(estadopublicacion);
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
        public async Task<IActionResult> ActualizarEstadoPublicacion([FromBody] EstadoPublicacion estadopublicacion)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (estadopublicacion == null || estadopublicacion.IdEstado <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _estadopublicacionrepository.GetEstadoPublicacionById(estadopublicacion.IdEstado);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. El estado de publicación con ID {estadopublicacion.IdEstado} no existe.");
                }

                exist.Nombre = estadopublicacion.Nombre;
                var response = await _estadopublicacionrepository.PutEstadoPublicacion(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEstadoPublicacion(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Estado de publicación no autenticado.");
                }

                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID del estado de publicación no es válido.");
                }

                var exist = await _estadopublicacionrepository.GetEstadoPublicacionById(id);

                if (exist == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede eliminar. El estado de publicación con ID {id} no existe."
                    );
                }

                var response = await _estadopublicacionrepository.DeleteEstadoPublicacion(exist);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }
    }
}
