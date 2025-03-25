using System.ComponentModel.DataAnnotations;

namespace Hackathon.Web.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O identificador é obrigatório")]
        public string Identificador { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;

        public string TipoUsuario { get; set; } = "Paciente"; // Default to Paciente, can be changed to "Medico"
    }
}