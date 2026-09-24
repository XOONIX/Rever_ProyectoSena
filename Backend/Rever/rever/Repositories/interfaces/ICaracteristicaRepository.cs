using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface ICaracteristicaRepository
    {
        Task<List<Caracteristica>> GetCaracteristica();
        Task<Caracteristica> GetCaracteristicaById(int id);
        Task<bool> PostCaracteristica(Caracteristica caracteristica);
        Task<bool> PutCaracteristica(Caracteristica caracteristica);
        Task<bool> DeleteCaracteristica(Caracteristica caracteristica);
    }
}
