using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IImagenRepository
    {
        Task<List<Imagen>> GetImagen();
        Task<Imagen> GetImagenById(int id);
        Task<bool> PostImagen(Imagen imagen);
        Task<bool> PutImagen(Imagen imagen);
        Task<bool> DeleteImagen(Imagen imagen);
    }
}