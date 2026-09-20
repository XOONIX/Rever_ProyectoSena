using Microsoft.EntityFrameworkCore;
using rever.contexto;
using rever.Dtos;
using rever.Models;
using rever.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace rever.Repositories
{
    public class InmuebleRepository : IInmuebleRepository
    {
        private readonly DatabaseService _context;

        public InmuebleRepository(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Inmueble>> GetInmueble()
        {
            var data = await _context.Inmueble.ToListAsync();
            return data;
        }

        public async Task<Inmueble> GetInmuebleById(int id)
        {
            var data = await _context.Inmueble.FirstOrDefaultAsync(x => x.IdInmueble == id);
            return data;
        }

        public async Task<bool> PostInmueble(Inmueble Inmueble)
        {
            await _context.Inmueble.AddAsync(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutInmueble(Inmueble Inmueble)
        {
            var exist = _context.Inmueble.FirstOrDefault(x => x.IdInmueble == Inmueble.IdInmueble);
            if (exist == null)
            {
                return false;
            }

            exist.Titulo = Inmueble.Titulo;
            exist.Descripcion = Inmueble.Descripcion;
            exist.Precio = Inmueble.Precio;
            exist.IdTipo = Inmueble.IdTipo;
            exist.Direccion = Inmueble.Direccion;
            exist.IdBarrio = Inmueble.IdBarrio;
            exist.Habitaciones = Inmueble.Habitaciones;
            exist.Baños = Inmueble.Baños;
            exist.MetrosCuadrados = Inmueble.MetrosCuadrados;
            exist.Estrato = Inmueble.Estrato;
            exist.Latitud = Inmueble.Latitud;
            exist.Longitud = Inmueble.Longitud;
            exist.IdUsuario = Inmueble.IdUsuario;
            exist.IdEstado = Inmueble.IdEstado;
            _context.Inmueble.Update(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInmueble(Inmueble Inmueble)
        {
            _context.Inmueble.Remove(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<InmuebleListadoDto>> GetListadoAsync()
        {
            return await _context.Inmueble
                .Include(i => i.Barrio).ThenInclude(b => b.Ciudad)
                .Include(i => i.TipoInmueble)
                .Include(i => i.ModoTransaccion)
                .Include(i => i.Imagenes)
                .Include(i => i.InmuebleCaracteristicas).ThenInclude(ic => ic.Caracteristica)
                .Select(i => new InmuebleListadoDto
                {
                    IdInmueble = i.IdInmueble,
                    Titulo = i.Titulo,
                    Precio = i.Precio,
                    Ubicacion = i.Barrio.Nombre + ", " + i.Barrio.Ciudad.Nombre,
                    Habitaciones = i.Habitaciones,
                    Banos = i.Baños,
                    MetrosCuadrados = i.MetrosCuadrados,
                    Tipo = i.TipoInmueble.Nombre,
                    Modo = i.ModoTransaccion.Nombre.ToLower(),
                    ImagenUrl = i.Imagenes.Select(img => img.Url).FirstOrDefault(),
                    Caracteristicas = i.InmuebleCaracteristicas.Select(ic => ic.Caracteristica.Nombre).ToList(),
                })
                .ToListAsync();
        }
    }
}