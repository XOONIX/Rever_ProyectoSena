using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class EstadoPublicacionController : ControllerBase
    {
        private readonly IEstadoPublicacionRepository _estadopublicacionrepository;
        public EstadoPublicacionController(IEstadoPublicacionRepository repository)
        {
            _estadopublicacionrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstadoPublicacion()
        {
            var response = await _estadopublicacionrepository.GetEstadoPublicacion();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoPublicacion(int id)
        {
            var response = await _estadopublicacionrepository.GetEstadoPublicacionById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearEstadoPublicacion([FromBody] EstadoPublicacion estadopublicacion)
        {
            var response = await _estadopublicacionrepository.PostEstadoPublicacion(estadopublicacion);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarEstadoPublicacion([FromBody] EstadoPublicacion estadopublicacion)
        {
            var response = await _estadopublicacionrepository.PutEstadoPublicacion(estadopublicacion);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarEstadoPublicacion(EstadoPublicacion estadopublicacion)
        {
            var response = await _estadopublicacionrepository.DeleteEstadoPublicacion(estadopublicacion);
            return Ok(response);
        }
    }
}
