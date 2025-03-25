using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.Entities
{
    public class Consulta
    {
        [Key]
        public long IdConsulta { get; set; }

        public long IdMedico { get; set; }
        public Medico Medico { get; set; } = null!;

        public long IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        [Required]
        public DateTime DataConsultaInicio { get; set; }

        [Required]
        public DateTime DataConsultaFim { get; set; }

        [Required]
        public decimal ValorConsulta { get; set; }

        [Required]
        public StatusConsulta Status { get; set; } = StatusConsulta.Pendente;

        [MaxLength(500)]
        public string? Justificativa { get; set; }
    }

    public enum StatusConsulta
    {
        Pendente = 0,
        Confirmada = 1,
        Recusada = 2,
        Cancelada = 3
    }
}
