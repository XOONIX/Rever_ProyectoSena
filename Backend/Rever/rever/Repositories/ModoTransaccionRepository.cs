using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace rever.Repositories
{
    public class ModoTransaccionRepository : IModoTransaccionRepository
    {
        private readonly DatabaseService _context;

        public ModoTransaccionRepository(DatabaseService context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModoTransaccion>> GetAllAsync()
        {
            return await _context.ModoTransaccion.ToListAsync();
        }

        public async Task<ModoTransaccion> GetByIdAsync(int id)
        {
            return await _context.ModoTransaccion.FindAsync(id);
        }

        public async Task<ModoTransaccion> CreateAsync(ModoTransaccion modo)
        {
            _context.ModoTransaccion.Add(modo);
            await _context.SaveChangesAsync();
            return modo;
        }

        public async Task<ModoTransaccion> UpdateAsync(ModoTransaccion modo)
        {
            _context.ModoTransaccion.Update(modo);
            await _context.SaveChangesAsync();
            return modo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var modo = await _context.ModoTransaccion.FindAsync(id);
            if (modo == null) return false;

            _context.ModoTransaccion.Remove(modo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}