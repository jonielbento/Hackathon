using Hackathon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IMedicoRepository
    {
        Task<IEnumerable<Medico>> GetAllAsync();
        Task<Medico> GetByIdAsync(long id);
        Task<Medico> CreateAsync(Medico medico);
        Task UpdateAsync(Medico medico);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}