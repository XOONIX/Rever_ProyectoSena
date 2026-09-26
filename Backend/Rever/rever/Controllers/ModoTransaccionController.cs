using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;
using rever.Repositories;
using rever.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace rever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "1")]
    public class ModoTransaccionController : ControllerBase
    {
        private readonly IModoTransaccionRepository _modotransaccionrepository;

        public ModoTransaccionController(IModoTransaccionRepository repository)
        {
            _modotransaccionrepository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var modos = await _modotransaccionrepository.GetAllAsync();
            return Ok(modos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var modo = await _modotransaccionrepository.GetByIdAsync(id);
            if (modo == null) return NotFound();
            return Ok(modo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModoTransaccion modo)
        {
            var creado = await _modotransaccionrepository.CreateAsync(modo);
            return CreatedAtAction(nameof(GetById), new { id = creado.IdModo }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ModoTransaccion modo)
        {
            if (id != modo.IdModo) return BadRequest();
            var actualizado = await _modotransaccionrepository.UpdateAsync(modo);
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _modotransaccionrepository.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}