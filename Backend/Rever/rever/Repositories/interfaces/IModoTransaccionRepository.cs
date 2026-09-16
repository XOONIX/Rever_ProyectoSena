using System.Collections.Generic;
using System.Threading.Tasks;
using rever.Models;

namespace rever.Repositories.Interfaces
{
    public interface IModoTransaccionRepository
    {
        Task<IEnumerable<ModoTransaccion>> GetAllAsync();
        Task<ModoTransaccion> GetByIdAsync(int id);
        Task<ModoTransaccion> CreateAsync(ModoTransaccion modo);
        Task<ModoTransaccion> UpdateAsync(ModoTransaccion modo);
        Task<bool> DeleteAsync(int id);
    }
}
