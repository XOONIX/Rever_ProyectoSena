using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class EstadoPublicacionRepository : IEstadoPublicacionRepository
    {
        private readonly DatabaseService _context;

        public EstadoPublicacionRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<EstadoPublicacion>> GetEstadoPublicacion()
        {
            var data = await _context.EstadoPublicacion.ToListAsync();
            return data;
        }

        public async Task<EstadoPublicacion> GetEstadoPublicacionById(int id)
        {
            var data = await _context.EstadoPublicacion.FirstOrDefaultAsync(x => x.IdEstado == id);
            return data;
        }

        public async Task<bool> PostEstadoPublicacion(EstadoPublicacion estadoPublicacion)
        {
            await _context.EstadoPublicacion.AddAsync(estadoPublicacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutEstadoPublicacion(EstadoPublicacion estadoPublicacion)
        {
            var exist = _context.EstadoPublicacion.FirstOrDefault(x => x.IdEstado == estadoPublicacion.IdEstado);
            if (exist == null)
            {
                return false;
            }

            exist.Nombre = estadoPublicacion.Nombre;
            _context.EstadoPublicacion.Update(estadoPublicacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEstadoPublicacion(EstadoPublicacion estadoPublicacion)
        {
            _context.EstadoPublicacion.Remove(estadoPublicacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}