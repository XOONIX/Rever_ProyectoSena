using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class InmuebleController : ControllerBase
    {
        private readonly IInmuebleRepository _inmueblerepository;
        public InmuebleController(IInmuebleRepository repository)
        {
            _inmueblerepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarInmueble()
        {
            var response = await _inmueblerepository.GetInmueble();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerInmueble(int id)
        {
            var response = await _inmueblerepository.GetInmuebleById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearInmueble([FromBody] Inmueble inmueble)
        {
            var response = await _inmueblerepository.PostInmueble(inmueble);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarInmueble([FromBody] Inmueble inmueble)
        {
            var response = await _inmueblerepository.PutInmueble(inmueble);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarInmueble(Inmueble inmueble)
        {
            var response = await _inmueblerepository.DeleteInmueble(inmueble);
            return Ok(response);
        }
    }
}
