using Hackathon.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Medico
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
    {
        return await _context.Medicos.Include(m => m.MedicoLogin)
                                     .Include(m => m.MedicoEspecialidades)
                                     .Include(m => m.HorariosTrabalho)
                                     .Include(m => m.Consultas)
                                     .ToListAsync();
    }

    // GET: api/Medico/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Medico>> GetMedico(long id)
    {
        var medico = await _context.Medicos.Include(m => m.MedicoLogin)
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
        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedico), new { id = medico.IdMedico }, medico);
    }

    // PUT: api/Medico/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedico(long id, Medico medico)
    {
        if (id != medico.IdMedico) return BadRequest();

        _context.Entry(medico).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Medico/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedico(long id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return NotFound();

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}