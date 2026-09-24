using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IEstadoPublicacionRepository
    {
        Task<List<EstadoPublicacion>> GetEstadoPublicacion();
        Task<EstadoPublicacion> GetEstadoPublicacionById(int id);
        Task<bool> PostEstadoPublicacion(EstadoPublicacion estadoPublicacion);
        Task<bool> PutEstadoPublicacion(EstadoPublicacion estadoPublicacion);
        Task<bool> DeleteEstadoPublicacion(EstadoPublicacion estadoPublicacion);
    }
}