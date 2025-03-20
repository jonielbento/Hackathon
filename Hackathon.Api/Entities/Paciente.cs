using System.ComponentModel.DataAnnotations;

namespace Hackathon.Api.Entities
{
    public class Paciente
    {
        [Key]
        public long IdPaciente { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; } = string.Empty;

        public PacienteLogin? PacienteLogin { get; set; }
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }

}
