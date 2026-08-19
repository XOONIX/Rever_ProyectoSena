using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class BarrioController : ControllerBase
    {
        private readonly IBarrioRepository _barriorrepository;
        public BarrioController(IBarrioRepository repository)
        {
            _barriorrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarBarrio()
        {
            var response = await _barriorrepository.GetBarrio();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerBarrio(int id)
        {
            var response = await _barriorrepository.GetBarrioById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearBarrio([FromBody] Barrio barrio)
        {
            var response = await _barriorrepository.PostBarrio(barrio);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarBarrio([FromBody] Barrio barrio)
        {
            var response = await _barriorrepository.PutBarrio(barrio);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarBarrio(Barrio barrio)
        {
            var response = await _barriorrepository.DeleteBarrio(barrio);
            return Ok(response);
        }
    }
}

