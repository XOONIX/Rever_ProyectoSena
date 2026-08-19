using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _Usuariorrepository;
        public UsuarioController(IUsuarioRepository repository)
        {
            _Usuariorrepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {
            var response = await _Usuariorrepository.GetUsuario();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var response = await _Usuariorrepository.GetUsuarioById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario Usuario)
        {
            var response = await _Usuariorrepository.PostUsuario(Usuario);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario Usuario)
        {
            var response = await _Usuariorrepository.PutUsuario(Usuario);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(Usuario Usuario)
        {
            var response = await _Usuariorrepository.DeleteUsuario(Usuario);
            return Ok(response);
        }
    }
}

