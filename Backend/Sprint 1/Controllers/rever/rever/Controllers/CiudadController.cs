using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadRepository _ciudadrepository;
        public CiudadController(ICiudadRepository repository)
        {
            _ciudadrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCiudad()
        {
            var response = await _ciudadrepository.GetCiudad();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCiudad(int id)
        {
            var response = await _ciudadrepository.GetCiudadById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCiudad([FromBody] Ciudad ciudad)
        {
            var response = await _ciudadrepository.PostCiudad(ciudad);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCiudad([FromBody] Ciudad ciudad)
        {
            var response = await _ciudadrepository.PutCiudad(ciudad);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCiudad(Ciudad ciudad)
        {
            var response = await _ciudadrepository.DeleteCiudad(ciudad);
            return Ok(response);
        }
    }
}

