using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IBarrioRepository
    {
        Task<List<Barrio>> GetBarrio();
        Task<Barrio> GetBarrioById(int id);
        Task<bool> PostBarrio(Barrio barrio);
        Task<bool> PutBarrio(Barrio barrio);
        Task<bool> DeleteBarrio(Barrio barrio);
    }
}
