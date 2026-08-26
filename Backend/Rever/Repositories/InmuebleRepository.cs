using Microsoft.EntityFrameworkCore;
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
            this._context =context;
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
    }
}