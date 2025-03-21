using Hackathon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options) {

    public DbSet<Medico> Medicos { get; set; }
    public DbSet<MedicoLogin> MedicoLogins { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<MedicoEspecialidade> MedicoEspecialidades { get; set; }
    public DbSet<MedicoHorarioTrabalho> MedicoHorariosTrabalho { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<PacienteLogin> PacienteLogins { get; set; }
    public DbSet<Consulta> Consultas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicoEspecialidade>().HasKey(me => new { me.IdMedico, me.IdEspecialidade });
    }
}