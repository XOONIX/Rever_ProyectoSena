using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories.Interfaces;

namespace rever.Repositories
{
    public class ImagenRepository : IImagenRepository
    {
        private readonly DatabaseService _context;

        public ImagenRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Imagen>> GetImagen()
        {
            var data = await _context.Imagen.ToListAsync();
            return data;
        }

        public async Task<Imagen> GetImagenById(int id)
        {
            var data = await _context.Imagen.FirstOrDefaultAsync(x => x.IdImagen == id);
            return data;
        }

        public async Task<bool> PostImagen(Imagen imagen)
        {
            await _context.Imagen.AddAsync(imagen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutImagen(Imagen imagen)
        {
            var exist = _context.Imagen.FirstOrDefault(x => x.IdImagen == imagen.IdImagen);
            if (exist == null)
            {
                return false;
            }

            _context.Imagen.Update(imagen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteImagen(Imagen imagen)
        {
            _context.Imagen.Remove(imagen);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}