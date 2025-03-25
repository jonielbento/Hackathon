using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteResponse>> GetAllPacientesAsync();
        Task<PacienteResponse> GetPacienteByIdAsync(long id);
        Task<PacienteResponse> CreatePacienteAsync(PacienteRequest pacienteRequest);
        Task UpdatePacienteAsync(long id, PacienteRequest pacienteRequest);
        Task<bool> DeletePacienteAsync(long id);
    }
}