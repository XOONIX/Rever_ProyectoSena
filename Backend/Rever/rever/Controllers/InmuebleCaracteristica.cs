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
    public class InmuebleCaracteristicaController : ControllerBase
    {
        private readonly IInmuebleCaracteristicaRepository _inmueblecaracteristicarepository;

        public InmuebleCaracteristicaController(IInmuebleCaracteristicaRepository repository)
        {
            _inmueblecaracteristicarepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarInmuebleCaracteristica()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                var response = await _inmueblecaracteristicarepository.GetInmuebleCaracteristica();
                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron características de inmuebles registradas.");
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        [HttpGet("{id1}/{id2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInmuebleCaracteristica(int id1, int id2)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (id1 <= 0 || id2 <= 0)
                {
                    return StatusCode(400, "400: Los IDs proporcionados no son válidos.");
                }

                var response = await _inmueblecaracteristicarepository.GetByIds(id1, id2);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró la relación entre el Inmueble {id1} y la Característica {id2}.");
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
        public async Task<IActionResult> CrearInmuebleCaracteristica([FromBody] InmuebleCaracteristica inmuebleCaracteristica)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (inmuebleCaracteristica == null)
                {
                    return StatusCode(400, "400: Los datos enviados no pueden ser nulos.");
                }

                var response = await _inmueblecaracteristicarepository.PostInmuebleCaracteristica(inmuebleCaracteristica);
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

        [HttpDelete("{id1},{id2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarInmuebleCaracteristica(int id1, int id2)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (id1 <= 0 || id2 <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o los IDs no son válidos.");
                }

                var exist = await _inmueblecaracteristicarepository.GetByIds(id1, id2);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede eliminar. La relación especificada no existe.");
                }

                var response = await _inmueblecaracteristicarepository.DeleteInmuebleCaracteristica(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }
    }
}