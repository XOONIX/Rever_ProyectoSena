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
        private readonly IInmuebleRepository _inmueblerepository;

        public InmuebleCaracteristicaController(
            IInmuebleCaracteristicaRepository repository,
            IInmuebleRepository inmuebleRepository)
        {
            _inmueblecaracteristicarepository = repository;
            _inmueblerepository = inmuebleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarInmuebleCaracteristica()
        {
            try
            {
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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerInmuebleCaracteristica(int id1, int id2)
        {
            try
            {
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
        [Authorize(Roles = "vendedor,administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearInmuebleCaracteristica([FromBody] InmuebleCaracteristica inmuebleCaracteristica)
        {
            try
            {
                if (inmuebleCaracteristica == null || inmuebleCaracteristica.IdInmueble <= 0)
                {
                    return StatusCode(400, "400: Los datos enviados no pueden ser nulos.");
                }

                var inmueble = await _inmueblerepository.GetInmuebleById(inmuebleCaracteristica.IdInmueble);
                if (inmueble == null)
                {
                    return StatusCode(404, $"404: El inmueble con ID {inmuebleCaracteristica.IdInmueble} no existe.");
                }

                if (!EsDuenoOAdmin(inmueble.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes agregar características a un inmueble que no es tuyo.");
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

        [HttpDelete("{id1}/{id2}")]
        [Authorize(Roles = "vendedor,administrador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarInmuebleCaracteristica(int id1, int id2)
        {
            try
            {
                if (id1 <= 0 || id2 <= 0)
                {
                    return StatusCode(400, "400: Los datos para eliminar o los IDs no son válidos.");
                }

                var exist = await _inmueblecaracteristicarepository.GetByIds(id1, id2);
                if (exist == null)
                {
                    return StatusCode(404, "404: No se puede eliminar. La relación especificada no existe.");
                }

                var inmueble = await _inmueblerepository.GetInmuebleById(id1);
                if (inmueble == null || !EsDuenoOAdmin(inmueble.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes eliminar características de un inmueble que no es tuyo.");
                }

                var response = await _inmueblecaracteristicarepository.DeleteInmuebleCaracteristica(exist);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"500 Error Interno: {ex.Message}");
            }
        }

        private bool EsDuenoOAdmin(int idVendedorDelInmueble)
        {
            // 1. Obtener el ID de forma segura como Nullable (int?)
            var idUsuarioToken = User.ObtenerIdUsuario();

            // 2. Si no hay token o no se pudo extraer el ID, no tiene acceso
            if (!idUsuarioToken.HasValue)
            {
                return false;
            }

            // 3. Verificar si el usuario es administrador (contemplando ambas variaciones de rol)
            var esAdmin = User.IsInRole("administrador") || User.IsInRole("Admin");

            // 4. Comparar el ID extraído (.Value) con el dueño del recurso
            return idVendedorDelInmueble == idUsuarioToken.Value || esAdmin;
        }
    }
}