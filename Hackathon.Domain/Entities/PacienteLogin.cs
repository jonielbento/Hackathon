using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.Domain.Entities
{
    public class PacienteLogin
    {
        [Key, ForeignKey("Paciente")]
        public long IdPaciente { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Senha { get; set; } = string.Empty;

        [Required, MaxLength(11)]
        public string Cpf { get; set; } = string.Empty;

        public Paciente Paciente { get; set; } = null!;
    }
}
