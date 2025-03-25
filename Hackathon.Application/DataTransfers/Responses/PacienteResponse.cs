using System.Collections.Generic;

namespace Hackathon.Application.DataTransfers.Responses
{
    public class PacienteResponse
    {
        public long IdPaciente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        
        // Não incluímos informações sensíveis como senha
        // Podemos incluir uma lista de IDs de consultas se necessário
        public IEnumerable<long>? ConsultasIds { get; set; }
    }
}