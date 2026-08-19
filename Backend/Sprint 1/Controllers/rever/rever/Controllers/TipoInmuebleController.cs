using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TipoInmuebleController : ControllerBase
    {
        private readonly ITipoInmuebleRepository _tipoinmueblerepository;
        public TipoInmuebleController(ITipoInmuebleRepository repository)
        {
            _tipoinmueblerepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTipoInmueble()
        {
            var response = await _tipoinmueblerepository.GetTipoInmueble();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTipoInmueble(int id)
        {
            var response = await _tipoinmueblerepository.GetTipoInmuebleById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoInmueble([FromBody] TipoInmueble tipoInmueble)
        {
            var response = await _tipoinmueblerepository.PostTipoInmueble(tipoInmueble);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarTipoInmueble([FromBody] TipoInmueble tipoInmueble)
        {
            var response = await _tipoinmueblerepository.PutTipoInmueble(tipoInmueble);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarTipoInmueble(TipoInmueble tipoInmueble)
        {
            var response = await _tipoinmueblerepository.DeleteTipoInmueble(tipoInmueble);
            return Ok(response);
        }
    }
}

