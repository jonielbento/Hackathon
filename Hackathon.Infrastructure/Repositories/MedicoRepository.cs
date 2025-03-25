using Hackathon.Application.Interfaces;
using Hackathon.Domain.Entities;
using Hackathon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly AppDbContext _context;

        public MedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medico>> GetAllAsync()
        {
            return await _context.Medicos
                .Include(m => m.MedicoLogin)
                .Include(m => m.MedicoEspecialidades)
                    .ThenInclude(me => me.Especialidade)
                .Include(m => m.HorariosTrabalho)
                .Include(m => m.Consultas)
                .ToListAsync();
        }

        public async Task<Medico> GetByIdAsync(long id)
        {
            return await _context.Medicos
                .Include(m => m.MedicoLogin)
                .Include(m => m.MedicoEspecialidades)
                    .ThenInclude(me => me.Especialidade)
                .Include(m => m.HorariosTrabalho)
                .Include(m => m.Consultas)
                .FirstOrDefaultAsync(m => m.IdMedico == id);
        }

        public async Task<Medico> CreateAsync(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task UpdateAsync(Medico medico)
        {
            _context.Entry(medico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
                return false;

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Medicos.AnyAsync(m => m.IdMedico == id);
        }
    }
}