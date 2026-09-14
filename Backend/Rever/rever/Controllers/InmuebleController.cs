using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
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

        [HttpGet]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearInmueble([FromBody] Inmueble inmueble)
        {
            try
            {
                if (inmueble == null)
                {
                    return StatusCode(400, "400: Los datos del inmueble no pueden ser nulos.");
                }

                // El dueño siempre es quien está logueado, nunca lo que mande el body
                inmueble.IdUsuario = ObtenerIdUsuarioToken();

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

        private int ObtenerIdUsuarioToken() => int.Parse(User.FindFirst("idUsuario")!.Value);

        private bool EsDuenoOAdmin(int idDuenoDelRecurso)
        {
            return idDuenoDelRecurso == ObtenerIdUsuarioToken() || User.IsInRole("administrador");
        }
    }
}