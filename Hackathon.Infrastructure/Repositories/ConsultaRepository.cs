using Hackathon.Application.Interfaces;
using Hackathon.Domain.Entities;
using Hackathon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;

        public ConsultaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> GetByMedicoIdAsync(long medicoId)
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Where(c => c.IdMedico == medicoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(long pacienteId)
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .Where(c => c.IdPaciente == pacienteId)
                .ToListAsync();
        }

        public async Task<Consulta> GetByIdAsync(long id)
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.IdConsulta == id);
        }

        public async Task<Consulta> CreateAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
                return false;

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Consultas.AnyAsync(c => c.IdConsulta == id);
        }
    }
}