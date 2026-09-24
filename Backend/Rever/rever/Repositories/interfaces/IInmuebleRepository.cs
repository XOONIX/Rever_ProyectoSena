using rever.Dtos;
using rever.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rever.Repositories.Interfaces
{
    public interface IInmuebleRepository
    {
        Task<List<Inmueble>> GetInmueble();
        Task<Inmueble> GetInmuebleById(int id);
        Task<bool> PostInmueble(Inmueble inmueble);
        Task<bool> PutInmueble(Inmueble inmueble);
        Task<bool> DeleteInmueble(Inmueble inmueble);
        Task<IEnumerable<InmuebleListadoDto>> GetListadoAsync();
    }
}