using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IHorarioTrabalhoService
    {
        Task<IEnumerable<HorarioTrabalhoResponse>> GetHorariosByMedicoIdAsync(long medicoId);
        Task<HorarioTrabalhoResponse> CreateHorarioAsync(long medicoId, HorarioTrabalhoRequest request);
        Task UpdateHorarioAsync(long medicoId, long horarioId, HorarioTrabalhoRequest request);
        Task<bool> DeleteHorarioAsync(long medicoId, long horarioId);
    }
}