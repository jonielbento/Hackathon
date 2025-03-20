using Hackathon.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        modelBuilder.Entity<MedicoEspecialidade>().HasKey(me => new { me.IdMedico, me.IdEspecialidade });
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionando serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
