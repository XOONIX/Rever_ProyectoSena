using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.contexto;
using rever.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace rever.Repositories
{
    public class CaracteristicaRepository : ICaracteristicaRepository
    {
        private readonly DatabaseService _context;

        public CaracteristicaRepository(DatabaseService context)
        {
            this._context =context;
        }

        public async Task<List<Caracteristica>> GetCaracteristica()
        {
            var data = await _context.Caracteristica.ToListAsync();
            return data;
        }

        public async Task<Caracteristica> GetCaracteristicaById(int id)
        {
            var data = await _context.Caracteristica.FirstOrDefaultAsync(x => x.IdCaracteristica == id);
            return data;
        }

        public async Task<bool> PostCaracteristica(Caracteristica caracteristica)
        {
            await _context.Caracteristica.AddAsync(caracteristica);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutCaracteristica(Caracteristica caracteristica)
        {
            var exist = _context.Caracteristica.FirstOrDefault(x => x.IdCaracteristica == caracteristica.IdCaracteristica);
            if (exist == null)
            {
                return false;
            }

            exist.Nombre = caracteristica.Nombre;
            _context.Caracteristica.Update(caracteristica);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCaracteristica(Caracteristica caracteristica)
        {
            _context.Caracteristica.Remove(caracteristica);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
