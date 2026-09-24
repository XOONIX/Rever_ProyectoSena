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
    public class ImagenController : ControllerBase
    {
        private readonly IImagenRepository _imagenrepository;
        private readonly IInmuebleRepository _inmueblerepository;

        public ImagenController(IImagenRepository repository, IInmuebleRepository inmuebleRepository)
        {
            _imagenrepository = repository;
            _inmueblerepository = inmuebleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarImagen()
        {
            try
            {
                var response = await _imagenrepository.GetImagen();
                if (response == null)
                {
                    return StatusCode(404, "404: No se encontraron imágenes registradas.");
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
        public async Task<IActionResult> ObtenerImagen(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID proporcionado no es válido.");
                }

                var response = await _imagenrepository.GetImagenById(id);
                if (response == null)
                {
                    return StatusCode(404, $"404: No se encontró la imagen con ID {id}.");
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
        public async Task<IActionResult> CrearImagen([FromBody] Imagen imagen)
        {
            try
            {
                if (imagen == null || imagen.IdInmueble <= 0)
                {
                    return StatusCode(400, "400: Los datos de la imagen no pueden ser nulos.");
                }

                var inmueble = await _inmuebleRepository_ObtenerInmueble(imagen.IdInmueble);
                if (inmueble == null)
                {
                    return StatusCode(404, $"404: El inmueble con ID {imagen.IdInmueble} no existe.");
                }

                if (!EsDuenoOAdmin(inmueble.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes agregar imágenes a un inmueble que no es tuyo.");
                }

                var response = await _imagenrepository.PostImagen(imagen);
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
        public async Task<IActionResult> ActualizarImagen([FromBody] Imagen imagen)
        {
            try
            {
                if (imagen == null || imagen.IdImagen <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _imagenrepository.GetImagenById(imagen.IdImagen);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. La imagen con ID {imagen.IdImagen} no existe.");
                }

                var inmueble = await _inmuebleRepository_ObtenerInmueble(exist.IdInmueble);
                if (inmueble == null || !EsDuenoOAdmin(inmueble.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes modificar imágenes de un inmueble que no es tuyo.");
                }

                exist.Url = imagen.Url;
                exist.IdInmueble = imagen.IdInmueble;
                var response = await _imagenrepository.PutImagen(exist);
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
        public async Task<IActionResult> DeleteImagen(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID de la imagen no es válido.");
                }

                var exist = await _imagenrepository.GetImagenById(id);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede eliminar. La imagen con ID {id} no existe.");
                }

                var inmueble = await _inmuebleRepository_ObtenerInmueble(exist.IdInmueble);
                if (inmueble == null || !EsDuenoOAdmin(inmueble.IdUsuario))
                {
                    return StatusCode(403, "403: No puedes eliminar imágenes de un inmueble que no es tuyo.");
                }

                var response = await _imagenrepository.DeleteImagen(exist);
                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }

        // ---- Helpers privados para no repetir la misma lógica cuatro veces ----

        private async Task<Inmueble?> _inmuebleRepository_ObtenerInmueble(int idInmueble)
        {
            return await _inmueblerepository.GetInmuebleById(idInmueble);
        }

        private bool EsDuenoOAdmin(int idVendedorDelInmueble)
        {
            // 1. Obtener el ID del token usando tu método de extensión
            var idUsuarioToken = User.ObtenerIdUsuario();

            // 2. Si no hay token o no tiene ID, denegar acceso
            if (!idUsuarioToken.HasValue)
            {
                return false;
            }

            // 3. Verificar si es administrador
            var esAdmin = User.IsInRole("administrador") || User.IsInRole("Admin");

            // 4. Verificar autoría comparando los enteros
            return idVendedorDelInmueble == idUsuarioToken.Value || esAdmin;
        }
    }
}
