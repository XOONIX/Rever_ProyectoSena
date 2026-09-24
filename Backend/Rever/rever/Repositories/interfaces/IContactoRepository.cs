using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IContactoRepository
    {
        Task<List<Contacto>> GetContacto();
        Task<Contacto> GetContactoById(int id);
        Task<bool> PostContacto(Contacto contacto);
        Task<bool> PutContacto(Contacto contacto);
        Task<bool> DeleteContacto(Contacto contacto);
    }
}
