using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class TipoInmuebleRepository : ITipoInmuebleRepository
    {
        private readonly DatabaseService _context;

        public TipoInmuebleRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<TipoInmueble>> GetTipoInmueble()
        {
            var data = await _context.TipoInmueble.ToListAsync();
            return data;
        }

        public async Task<TipoInmueble> GetTipoInmuebleById(int id)
        {
            var data = await _context.TipoInmueble.FirstOrDefaultAsync(x => x.IdTipo == id);
            return data;
        }

        public async Task<bool> PostTipoInmueble(TipoInmueble tipoInmueble)
        {
            await _context.TipoInmueble.AddAsync(tipoInmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutTipoInmueble(TipoInmueble tipoInmueble)
        {
            var exist = _context.TipoInmueble.FirstOrDefault(x => x.IdTipo == tipoInmueble.IdTipo);
            if (exist == null)
            {
                return false;
            }

            exist.Nombre = tipoInmueble.Nombre;
            _context.TipoInmueble.Update(tipoInmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTipoInmueble(TipoInmueble tipoInmueble)
        {
            _context.TipoInmueble.Remove(tipoInmueble);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
