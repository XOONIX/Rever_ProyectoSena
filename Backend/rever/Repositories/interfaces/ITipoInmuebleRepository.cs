using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface ITipoInmuebleRepository
    {
        Task<List<TipoInmueble>> GetTipoInmueble();
        Task<TipoInmueble> GetTipoInmuebleById(int id);
        Task<bool> PostTipoInmueble(TipoInmueble tipoInmueble);
        Task<bool> PutTipoInmueble(TipoInmueble tipoInmueble);
        Task<bool> DeleteTipoInmueble(TipoInmueble tipoInmueble);
    }
}
