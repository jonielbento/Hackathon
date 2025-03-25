using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IConsultaService
    {
        Task<IEnumerable<ConsultaResponse>> GetConsultasByMedicoIdAsync(long medicoId);
        Task<IEnumerable<ConsultaResponse>> GetConsultasByPacienteIdAsync(long pacienteId);
        Task<ConsultaResponse> GetConsultaByIdAsync(long consultaId);
        Task<ConsultaResponse> AgendarConsultaAsync(AgendarConsultaRequest request);
        Task<ConsultaResponse> AtualizarStatusConsultaAsync(long consultaId, AtualizarStatusConsultaRequest request);
    }
}