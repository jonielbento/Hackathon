using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using Hackathon.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // GET: api/Medico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoResponse>>> GetMedicos()
        {
            return Ok(await _medicoService.GetAllMedicosAsync());
        }

        // GET: api/Medico/{id}
        [HttpGet("{id:long}")]
        public async Task<ActionResult<MedicoResponse>> GetMedico(long id)
        {
            var medico = await _medicoService.GetMedicoByIdAsync(id);
            if (medico == null)
                return NotFound();

            return medico;
        }

        // POST: api/Medico
        [HttpPost]
        public async Task<ActionResult<MedicoResponse>> PostMedico(MedicoRequest medicoRequest)
        {
            var medico = await _medicoService.CreateMedicoAsync(medicoRequest);
            return CreatedAtAction(nameof(GetMedico), new { id = medico.IdMedico }, medico);
        }

        // PUT: api/Medico/{id}
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutMedico(long id, MedicoRequest medicoRequest)
        {
            try
            {
                await _medicoService.UpdateMedicoAsync(id, medicoRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Medico/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteMedico(long id)
        {
            var result = await _medicoService.DeleteMedicoAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}