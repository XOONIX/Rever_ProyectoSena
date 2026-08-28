using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface ICiudadRepository
    {
        Task<List<Ciudad>> GetCiudad();
        Task<Ciudad> GetCiudadById(int id);
        Task<bool> PostCiudad(Ciudad ciudad);
        Task<bool> PutCiudad(Ciudad ciudad);
        Task<bool> DeleteCiudad(Ciudad ciudad);
    }
}
