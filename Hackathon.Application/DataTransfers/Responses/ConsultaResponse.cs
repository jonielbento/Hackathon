using Hackathon.Domain.Entities;
using System;

namespace Hackathon.Application.DataTransfers.Responses
{
    public class ConsultaResponse
    {
        public long IdConsulta { get; set; }
        public long IdMedico { get; set; }
        public string NomeMedico { get; set; } = string.Empty;
        public string CrmMedico { get; set; } = string.Empty;
        public long IdPaciente { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public DateTime DataConsultaInicio { get; set; }
        public DateTime DataConsultaFim { get; set; }
        public decimal ValorConsulta { get; set; }
        public StatusConsulta Status { get; set; }
        public string? Justificativa { get; set; }
    }
}