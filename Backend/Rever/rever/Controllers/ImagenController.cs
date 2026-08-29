using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ImagenController : ControllerBase
    {
        private readonly IImagenRepository _imagenrepository;

        public ImagenController(IImagenRepository repository)
        {
            _imagenrepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarImagen()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerImagen(int id)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearImagen([FromBody] Imagen imagen)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (imagen == null)
                {
                    return StatusCode(400, "400: Los datos de la imagen no pueden ser nulos.");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarImagen([FromBody] Imagen imagen)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Usuario no autenticado.");
                }

                if (imagen == null || imagen.IdImagen <= 0)
                {
                    return StatusCode(400, "400: Los datos para actualizar o el ID no son válidos.");
                }

                var exist = await _imagenrepository.GetImagenById(imagen.IdImagen);
                if (exist == null)
                {
                    return StatusCode(404, $"404: No se puede actualizar. La imagen con ID {imagen.IdImagen} no existe.");
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteImagen(int id)
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return StatusCode(401, "401: Imagen no autenticada.");
                }

                if (id <= 0)
                {
                    return StatusCode(400, "400: El ID de la imagen no es válido.");
                }

                var exist = await _imagenrepository.GetImagenById(id);

                if (exist == null)
                {
                    return StatusCode(
                        404,
                        $"404: No se puede eliminar. La imagen con ID {id} no existe."
                    );
                }

                var response = await _imagenrepository.DeleteImagen(exist);

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                return StatusCode(500, "500: Error interno del servidor.");
            }
        }
    }
}

