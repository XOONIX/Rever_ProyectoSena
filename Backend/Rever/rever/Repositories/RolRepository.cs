using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DatabaseService _context;

        public RolRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Rol>> GetRol()
        {
            var data = await _context.Rol.ToListAsync();
            return data;
        }

        public async Task<Rol> GetRolById(int id)
        {
            var data = await _context.Rol.FirstOrDefaultAsync(x => x.IdRol == id);
            return data;
        }

        public async Task<bool> PostRol(Rol rol)
        {
            await _context.Rol.AddAsync(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutRol(Rol rol)
        {
            var exist = _context.Rol.FirstOrDefault(x => x.IdRol == rol.IdRol);
            if (exist == null)
            {
                return false;
            }

            exist.Nombre = rol.Nombre;
            _context.Rol.Update(rol);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteRol(Rol rol)
        {
            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
