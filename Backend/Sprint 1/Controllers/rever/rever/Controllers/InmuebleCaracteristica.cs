using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class InmuebleCaracteristicaController : ControllerBase
    {
        private readonly IInmuebleCaracteristicaRepository _inmueblecaracteristicarepository;

        public InmuebleCaracteristicaController(IInmuebleCaracteristicaRepository repository)
        {
            _inmueblecaracteristicarepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarInmuebleCaracteristica()
        {
            var response = await _inmueblecaracteristicarepository.GetInmuebleCaracteristica();
            return Ok(response);
        }

        // Corregido: recibe ambas llaves
        [HttpGet("{idInmueble}/{idCaracteristica}")]
        public async Task<IActionResult> ObtenerInmuebleCaracteristica(int idInmueble, int idCaracteristica)
        {
            var response = await _inmueblecaracteristicarepository.GetByIds(idInmueble, idCaracteristica);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearInmuebleCaracteristica([FromBody] InmuebleCaracteristica inmuebleCaracteristica)
        {
            var response = await _inmueblecaracteristicarepository.PostInmuebleCaracteristica(inmuebleCaracteristica);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarInmuebleCaracteristica([FromBody] InmuebleCaracteristica inmuebleCaracteristica)
        {
            var response = await _inmueblecaracteristicarepository.DeleteInmuebleCaracteristica(inmuebleCaracteristica);
            return Ok(response);
        }
    }
}