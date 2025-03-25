using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<IEnumerable<MedicoResponse>> GetAllMedicosAsync();
        Task<MedicoResponse> GetMedicoByIdAsync(long id);
        Task<MedicoResponse> CreateMedicoAsync(MedicoRequest medicoRequest);
        Task UpdateMedicoAsync(long id, MedicoRequest medicoRequest);
        Task<bool> DeleteMedicoAsync(long id);
        
        // Novo método para busca com filtros
        Task<IEnumerable<MedicoResponse>> BuscarMedicosAsync(BuscarMedicosRequest request);
    }
}