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
    public class TipoInmuebleController : ControllerBase
    {
        private readonly ITipoInmuebleRepository _tipoinmueblerepository;

        public TipoInmuebleController(ITipoInmuebleRepository repository)
        {
            _tipoinmueblerepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTipoInmueble()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                var response = await _tipoinmueblerepository.GetTipoInmueble();

                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron tipos de inmueble registrados.");
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
        public async Task<IActionResult> ObtenerTipoInmueble(int id)
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

                var existe = await _tipoinmueblerepository.GetTipoInmuebleById(id);

                if (existe == null)
                {
                    return StatusCode(404, $"404: No se encontró el tipo de inmueble con ID {id}.");
                }

                return StatusCode(200, existe);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearTipoInmueble([FromBody] TipoInmueble tipoInmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (tipoInmueble == null)
                {
                    return StatusCode(400, "400: Los datos del tipo de inmueble no pueden ser nulos.");
                }

                var response = await _tipoinmueblerepository.PostTipoInmueble(tipoInmueble);

                if (response == null)
                {
                    return StatusCode(500, "500: Error interno al intentar crear el recurso.");
                }

                return StatusCode(200, response);
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
        public async Task<IActionResult> ActualizarTipoInmueble([FromBody] TipoInmueble tipoInmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (tipoInmueble == null || tipoInmueble.IdTipo <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var existe = await _tipoinmueblerepository.GetTipoInmuebleById(
                    tipoInmueble.IdTipo);

                if (existe == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede actualizar. El tipo de inmueble con ID {tipoInmueble.IdTipo} no existe."
                    );
                }

                var response = await _tipoinmueblerepository.PutTipoInmueble(tipoInmueble);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarTipoInmueble([FromBody] TipoInmueble tipoInmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (tipoInmueble == null || tipoInmueble.IdTipo <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o el ID no son válidos.");
                }

                var existe = await _tipoinmueblerepository.GetTipoInmuebleById(
                    tipoInmueble.IdTipo);

                if (existe == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede eliminar. El tipo de inmueble con ID {tipoInmueble.IdTipo} no existe."
                    );
                }

                var response = await _tipoinmueblerepository.DeleteTipoInmueble(existe);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }
    }
}
