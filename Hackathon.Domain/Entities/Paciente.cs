using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.Entities
{
    public class Paciente
    {
        [Key]
        public long IdPaciente { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(14)]
        public string Cpf { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Telefone { get; set; } = string.Empty;

        public PacienteLogin? PacienteLogin { get; set; }
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
