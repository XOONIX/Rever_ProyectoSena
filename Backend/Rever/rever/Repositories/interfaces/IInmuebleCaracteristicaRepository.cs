using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IInmuebleCaracteristicaRepository
    {
        Task<List<InmuebleCaracteristica>> GetInmuebleCaracteristica();
        Task<InmuebleCaracteristica> GetByIds(int idInmueble, int idCaracteristica);
        Task<bool> PostInmuebleCaracteristica(InmuebleCaracteristica inmuebleCaracteristica);
        Task<bool> DeleteInmuebleCaracteristica(InmuebleCaracteristica inmuebleCaracteristica);
    }
}
