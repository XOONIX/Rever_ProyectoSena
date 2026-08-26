using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace rever.Repositories
{
    public class BarrioRepository : IBarrioRepository
    {
        private readonly DatabaseService _context;

        public BarrioRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Barrio>> GetBarrio()
        {
            var data = await _context.Barrio.ToListAsync();
            return data;
        }

        public async Task<Barrio> GetBarrioById(int id)
        {
            var data = await _context.Barrio.FirstOrDefaultAsync(x => x.IdBarrio == id);
            return data;
        }

        public async Task<bool> PostBarrio(Barrio barrio)
        {
            await _context.Barrio.AddAsync(barrio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutBarrio(Barrio barrio)
        {
            var exist = _context.Barrio.FirstOrDefault(x => x.IdBarrio == barrio.IdBarrio);
            if (exist == null)
            {
                return false;
            }

            _context.Barrio.Update(barrio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBarrio(Barrio barrio)
        {
            _context.Barrio.Remove(barrio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
