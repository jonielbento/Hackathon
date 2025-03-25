using Hackathon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente> GetByIdAsync(long id);
        Task<Paciente> CreateAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}