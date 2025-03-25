using Hackathon.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Application.DataTransfers.Requests
{
    public class AtualizarStatusConsultaRequest
    {
        [Required(ErrorMessage = "O status é obrigatório")]
        public StatusConsulta Status { get; set; }

        [MaxLength(500, ErrorMessage = "A justificativa não pode exceder 500 caracteres")]
        public string? Justificativa { get; set; }
    }
}