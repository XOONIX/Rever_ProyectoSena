using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CaracteristicaController : ControllerBase
    {
        private readonly ICaracteristicaRepository _caracteristicarepository;
        public CaracteristicaController(ICaracteristicaRepository repository)
        {
            _caracteristicarepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCaracteristica()
        {
            var response = await _caracteristicarepository.GetCaracteristica();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCaracteristica(int id)
        {
            var response = await _caracteristicarepository.GetCaracteristicaById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCaracteristica([FromBody] Caracteristica caracteristica)
        {
            var response = await _caracteristicarepository.PostCaracteristica(caracteristica);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCaracteristica([FromBody] Caracteristica caracteristica)
        {
            var response = await _caracteristicarepository.PutCaracteristica(caracteristica);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCaracteristica(Caracteristica caracteristica)
        {
            var response = await _caracteristicarepository.DeleteCaracteristica(caracteristica);
            return Ok(response);
        }
    }
}
