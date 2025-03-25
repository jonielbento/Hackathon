using Hackathon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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
            // Configuração de relacionamentos
            modelBuilder.Entity<MedicoEspecialidade>()
                .HasKey(me => new { me.IdMedico, me.IdEspecialidade });

            modelBuilder.Entity<MedicoEspecialidade>()
                .HasOne(me => me.Medico)
                .WithMany(m => m.MedicoEspecialidades)
                .HasForeignKey(me => me.IdMedico);

            modelBuilder.Entity<MedicoEspecialidade>()
                .HasOne(me => me.Especialidade)
                .WithMany(e => e.MedicoEspecialidades)
                .HasForeignKey(me => me.IdEspecialidade);

            // Configuração para Consultas
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.IdMedico);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.IdPaciente);
        }
    }
}