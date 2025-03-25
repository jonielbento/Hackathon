using System.ComponentModel.DataAnnotations;

namespace Hackathon.Application.DataTransfers.Requests
{
    public class PacienteLoginRequest
    {
        // Pode ser email ou CPF
        [Required(ErrorMessage = "O identificador (Email ou CPF) é obrigatório")]
        public string Identificador { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;
    }
}