using Hackathon.Domain.Entities;
using Hackathon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicoController(AppDbContext context) : ControllerBase{
    // GET: api/Medico
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
    {
        return await context.Medicos.Include(m => m.MedicoLogin)
            .Include(m => m.MedicoEspecialidades)
            .Include(m => m.HorariosTrabalho)
            .Include(m => m.Consultas)
            .ToListAsync();
    }

    // GET: api/Medico/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Medico>> GetMedico(long id)
    {
        var medico = await context.Medicos.Include(m => m.MedicoLogin)
            .Include(m => m.MedicoEspecialidades)
            .Include(m => m.HorariosTrabalho)
            .Include(m => m.Consultas)
            .FirstOrDefaultAsync(m => m.IdMedico == id);

        if (medico == null) return NotFound();

        return medico;
    }

    // POST: api/Medico
    [HttpPost]
    public async Task<ActionResult<Medico>> PostMedico(Medico medico)
    {
        context.Medicos.Add(medico);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedico), new { id = medico.IdMedico }, medico);
    }

    // PUT: api/Medico/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedico(long id, Medico medico)
    {
        if (id != medico.IdMedico) return BadRequest();

        context.Entry(medico).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Medico/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedico(long id)
    {
        var medico = await context.Medicos.FindAsync(id);
        if (medico == null) return NotFound();

        context.Medicos.Remove(medico);
        await context.SaveChangesAsync();

        return NoContent();
    }
}