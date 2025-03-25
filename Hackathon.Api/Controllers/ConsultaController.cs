using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hackathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet("medico/{medicoId}")]
        public async Task<IActionResult> GetConsultasByMedico(long medicoId)
        {
            var consultas = await _consultaService.GetConsultasByMedicoIdAsync(medicoId);
            return Ok(consultas);
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> GetConsultasByPaciente(long pacienteId)
        {
            var consultas = await _consultaService.GetConsultasByPacienteIdAsync(pacienteId);
            return Ok(consultas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsulta(long id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);
            if (consulta == null)
                return NotFound();

            return Ok(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> AgendarConsulta(AgendarConsultaRequest request)
        {
            var consulta = await _consultaService.AgendarConsultaAsync(request);
            return CreatedAtAction(nameof(GetConsulta), new { id = consulta.IdConsulta }, consulta);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatusConsulta(long id, AtualizarStatusConsultaRequest request)
        {
            var consulta = await _consultaService.AtualizarStatusConsultaAsync(id, request);
            if (consulta == null)
                return NotFound();

            return Ok(consulta);
        }
    }
}