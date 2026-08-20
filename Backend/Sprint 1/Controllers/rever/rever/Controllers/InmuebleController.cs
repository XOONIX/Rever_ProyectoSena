using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarInmueble()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInmueble(int id)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (inmueble == null)
                {
                    return StatusCode(400, "400: Los datos del inmueble no pueden ser nulos.");
                }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (inmueble == null || inmueble.IdInmueble <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var existe = await _inmueblerepository.GetInmuebleById(inmueble.IdInmueble);
                if (existe == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. El inmueble con ID {inmueble.IdInmueble} no existe.");
                }

                var response = await _inmueblerepository.PutInmueble(inmueble);
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
        public async Task<IActionResult> EliminarInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (inmueble == null || inmueble.IdInmueble <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o el ID no son válidos.");
                }

                var existe = await _inmueblerepository.GetInmuebleById(inmueble.IdInmueble);
                if (existe == null)
                {
                    return StatusCode(404, $"404: No se puede eliminar. El inmueble con ID {inmueble.IdInmueble} no existe.");
                }

                var response = await _inmueblerepository.DeleteInmueble(existe);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }
    }
}
