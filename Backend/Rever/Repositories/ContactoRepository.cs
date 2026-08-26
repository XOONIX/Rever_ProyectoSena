using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly DatabaseService _context;

        public ContactoRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Contacto>> GetContacto()
        {
            var data = await _context.Contacto.ToListAsync();
            return data;
        }

        public async Task<Contacto> GetContactoById(int id)
        {
            var data = await _context.Contacto.FirstOrDefaultAsync(x => x.IdContacto == id);
            return data;
        }

        public async Task<bool> PostContacto(Contacto contacto)
        {
            await _context.Contacto.AddAsync(contacto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutContacto(Contacto contacto)
        {
            var exist = _context.Contacto.FirstOrDefault(x => x.IdContacto == contacto.IdContacto);
            if (exist == null)
            {
                return false;
            }

            _context.Contacto.Update(contacto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteContacto(Contacto contacto)
        {
            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
