using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly ILocalidadRepository _localidadrepository;
        public LocalidadController(ILocalidadRepository repository)
        {
            _localidadrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarLocalidad()
        {
            var response = await _localidadrepository.GetLocalidad();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerLocalidad(int id)
        {
            var response = await _localidadrepository.GetLocalidadById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearLocalidad([FromBody] Localidad localidad)
        {
            var response = await _localidadrepository.PostLocalidad(localidad);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarLocalidad([FromBody] Localidad localidad)
        {
            var response = await _localidadrepository.PutLocalidad(localidad);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarLocalidad(Localidad localidad)
        {
            var response = await _localidadrepository.DeleteLocalidad(localidad);
            return Ok(response);
        }
    }
}

