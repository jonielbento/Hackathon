using System.ComponentModel.DataAnnotations;

namespace Hackathon.Application.DataTransfers.Requests
{
    public class MedicoLoginRequest
    {
        [Required(ErrorMessage = "O CRM é obrigatório")]
        public string Crm { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;
    }
}