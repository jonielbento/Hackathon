using System;

namespace Hackathon.Web.Models
{
    public enum StatusConsulta
    {
        Pendente,
        Confirmada,
        Cancelada,
        Realizada
    }

    public class ConsultaModel
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