using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class InmuebleCaracteristicaRepository : IInmuebleCaracteristicaRepository
    {
        private readonly DatabaseService _context;

        public InmuebleCaracteristicaRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<InmuebleCaracteristica>> GetInmuebleCaracteristica()
        {
            var data = await _context.InmuebleCaracteristica.ToListAsync();
            return data;
        }

        public async Task<InmuebleCaracteristica> GetByIds(int idInmueble, int idCaracteristica)
        {
            var data = await _context.InmuebleCaracteristica
                .FirstOrDefaultAsync(x => x.IdInmueble == idInmueble && x.IdCaracteristica == idCaracteristica);
            return data;
        }

        public async Task<bool> PostInmuebleCaracteristica(InmuebleCaracteristica inmuebleCaracteristica)
        {
            await _context.InmuebleCaracteristica.AddAsync(inmuebleCaracteristica);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInmuebleCaracteristica(InmuebleCaracteristica inmuebleCaracteristica)
        {
            _context.InmuebleCaracteristica.Remove(inmuebleCaracteristica);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
