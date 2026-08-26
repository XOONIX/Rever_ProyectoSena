using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class CiudadRepository : ICiudadRepository
    {
        private readonly DatabaseService _context;

        public CiudadRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Ciudad>> GetCiudad()
        {
            var data = await _context.Ciudad.ToListAsync();
            return data;
        }

        public async Task<Ciudad> GetCiudadById(int id)
        {
            var data = await _context.Ciudad.FirstOrDefaultAsync(x => x.IdCiudad == id);
            return data;
        }

        public async Task<bool> PostCiudad(Ciudad ciudad)
        {
            await _context.Ciudad.AddAsync(ciudad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutCiudad(Ciudad ciudad)
        {
            _context.Ciudad.Update(ciudad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCiudad(Ciudad ciudad)
        {
            _context.Ciudad.Remove(ciudad);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
