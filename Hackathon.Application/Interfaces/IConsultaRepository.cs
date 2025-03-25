using Hackathon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetAllAsync();
        Task<IEnumerable<Consulta>> GetByMedicoIdAsync(long medicoId);
        Task<IEnumerable<Consulta>> GetByPacienteIdAsync(long pacienteId);
        Task<Consulta> GetByIdAsync(long id);
        Task<Consulta> CreateAsync(Consulta consulta);
        Task UpdateAsync(Consulta consulta);
        Task<bool> DeleteAsync(long id);
    }
}