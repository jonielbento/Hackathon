using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hackathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("medico/login")]
        public async Task<IActionResult> LoginMedico(MedicoLoginRequest request)
        {
            var response = await _authService.LoginMedicoAsync(request);
            if (!response.Sucesso)
                return Unauthorized(response);

            return Ok(response);
        }

        [HttpPost("paciente/login")]
        public async Task<IActionResult> LoginPaciente(PacienteLoginRequest request)
        {
            var response = await _authService.LoginPacienteAsync(request);
            if (!response.Sucesso)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}