using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginMedicoAsync(MedicoLoginRequest request);
        Task<LoginResponse> LoginPacienteAsync(PacienteLoginRequest request);
    }
}