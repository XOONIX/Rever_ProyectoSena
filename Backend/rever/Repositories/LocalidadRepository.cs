using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class LocalidadRepository : ILocalidadRepository
    {
        private readonly DatabaseService _context;

        public LocalidadRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Localidad>> GetLocalidad()
        {
            var data = await _context.Localidad.ToListAsync();
            return data;
        }

        public async Task<Localidad> GetLocalidadById(int id)
        {
            var data = await _context.Localidad.FirstOrDefaultAsync(x => x.IdLocalidad == id);
            return data;
        }

        public async Task<bool> PostLocalidad(Localidad localidad)
        {
            await _context.Localidad.AddAsync(localidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutLocalidad(Localidad localidad)
        {
            _context.Localidad.Update(localidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLocalidad(Localidad localidad)
        {
            _context.Localidad.Remove(localidad);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
