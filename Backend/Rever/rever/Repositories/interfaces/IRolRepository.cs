using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetRol();
        Task<Rol> GetRolById(int id);
        Task<bool> PostRol(Rol rol);
        Task<bool> PutRol(Rol rol);
        Task<bool> DeleteRol(Rol rol);
    }
}
