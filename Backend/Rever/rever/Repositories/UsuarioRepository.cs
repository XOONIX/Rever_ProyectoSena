using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseService _context;

        public UsuarioRepository(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Usuario>> GetUsuario()
        {
            var data = await _context.Usuario.ToListAsync();
            return data;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            var data = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            return data;
        }

        public async Task<Usuario?> GetByEmailWithRolAsync(string correo)
        {
            return await _context.Usuario.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<bool> PostUsuario(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutUsuario(Usuario usuario)
        {
            var exist = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == usuario.IdUsuario);
            if (exist == null)
            {
                return false;
            }

            exist.Nombre = usuario.Nombre;
            exist.Correo = usuario.Correo;
            exist.Contraseña = usuario.Contraseña;
            exist.Telefono = usuario.Telefono;
            exist.IdRol = usuario.IdRol;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUsuario(Usuario usuario)
        {
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
