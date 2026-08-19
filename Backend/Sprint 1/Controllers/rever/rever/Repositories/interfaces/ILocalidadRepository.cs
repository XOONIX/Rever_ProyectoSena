using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface ILocalidadRepository
    {
        Task<List<Localidad>> GetLocalidad();
        Task<Localidad> GetLocalidadById(int id);
        Task<bool> PostLocalidad(Localidad localidad);
        Task<bool> PutLocalidad(Localidad localidad);
        Task<bool> DeleteLocalidad(Localidad localidad);
    }
}
