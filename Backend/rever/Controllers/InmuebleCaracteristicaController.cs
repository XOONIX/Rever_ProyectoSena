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

        [HttpGet("{idInmueble:int}/{idCaracteristica:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInmuebleCaracteristica(int idInmueble, int idCaracteristica)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (idInmueble <= 0 || idCaracteristica <= 0)
                {
                    return StatusCode(400, "400: Los IDs proporcionados no son válidos.");
                }

                var response = await _inmueblecaracteristicarepository.GetByIds(idInmueble, idCaracteristica);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró la relación entre el Inmueble {idInmueble} y la Característica {idCaracteristica}.");
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarInmuebleCaracteristica(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (inmuebleCaracteristica == null || inmuebleCaracteristica.IdInmueble <= 0 || inmuebleCaracteristica.IdCaracteristica <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o los IDs no son válidos.");
                }

                var existe = await _inmueblecaracteristicarepository.GetByIds(inmuebleCaracteristica.IdInmueble, inmuebleCaracteristica.IdCaracteristica);
                if (existe == null)
                {
                    return StatusCode(404, $"404: No se puede eliminar. La relación especificada no existe.");
                }

                var response = await _inmueblecaracteristicarepository.DeleteInmuebleCaracteristica(existe);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }
    }
}
