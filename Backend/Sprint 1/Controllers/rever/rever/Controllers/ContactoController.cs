using rever.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using Microsoft.AspNetCore.Authorization;

namespace rever.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoRepository _contactorepository;
        public ContactoController(IContactoRepository repository)
        {
            _contactorepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarContacto()
        {
            var response = await _contactorepository.GetContacto();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerContacto(int id)
        {
            var response = await _contactorepository.GetContactoById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearContacto([FromBody] Contacto contacto)
        {
            var response = await _contactorepository.PostContacto(contacto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarContacto([FromBody] Contacto contacto)
        {
            var response = await _contactorepository.PutContacto(contacto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarContacto(Contacto contacto)
        {
            var response = await _contactorepository.DeleteContacto(contacto);
            return Ok(response);
        }
    }
}
