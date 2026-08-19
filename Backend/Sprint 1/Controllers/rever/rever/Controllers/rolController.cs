using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolrepository;
        public RolController(IRolRepository repository)
        {
            _rolrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarRol()
        {
            var response = await _rolrepository.GetRol();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRol(int id)
        {
            var response = await _rolrepository.GetRolById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] Rol rol)
        {
            var response = await _rolrepository.PostRol(rol);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarRol([FromBody] Rol rol)
        {
            var response = await _rolrepository.PutRol(rol);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarRol(Rol rol)
        {
            var response = await _rolrepository.DeleteRol(rol);
            return Ok(response);
        }
    }
}
}
