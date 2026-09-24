using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetUsuario();
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario?> GetByEmailWithRolAsync(string correo);
        Task<bool> PostUsuario(Usuario usuario);
        Task<bool> PutUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(Usuario usuario);
    }
}
