using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenRepository _imagenrepository;
        public ImagenController(IImagenRepository repository)
        {
            _imagenrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarImagen()
        {
            var response = await _imagenrepository.GetImagen();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerImagen(int id)
        {
            var response = await _imagenrepository.GetImagenById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearImagen([FromBody] Imagen imagen)
        {
            var response = await _imagenrepository.PostImagen(imagen);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarImagen([FromBody] Imagen imagen)
        {
            var response = await _imagenrepository.PutImagen(imagen);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarImagen(Imagen imagen)
        {
            var response = await _imagenrepository.DeleteImagen(imagen);
            return Ok(response);
        }
    }
}
