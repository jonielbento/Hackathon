using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hackathon.Api.Controllers
{
    [Route("api/medicos/{medicoId}/horarios")]
    [ApiController]
    [Authorize]
    public class HorarioTrabalhoController : ControllerBase
    {
        private readonly IHorarioTrabalhoService _horarioService;

        public HorarioTrabalhoController(IHorarioTrabalhoService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHorarios(long medicoId)
        {
            var horarios = await _horarioService.GetHorariosByMedicoIdAsync(medicoId);
            return Ok(horarios);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHorario(long medicoId, HorarioTrabalhoRequest request)
        {
            var horario = await _horarioService.CreateHorarioAsync(medicoId, request);
            return CreatedAtAction(nameof(GetHorarios), new { medicoId }, horario);
        }

        [HttpPut("{horarioId}")]
        public async Task<IActionResult> UpdateHorario(long medicoId, long horarioId, HorarioTrabalhoRequest request)
        {
            await _horarioService.UpdateHorarioAsync(medicoId, horarioId, request);
            return NoContent();
        }

        [HttpDelete("{horarioId}")]
        public async Task<IActionResult> DeleteHorario(long medicoId, long horarioId)
        {
            var result = await _horarioService.DeleteHorarioAsync(medicoId, horarioId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}