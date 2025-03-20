using System.ComponentModel.DataAnnotations;

namespace Hackathon.Api.Entities
{
    public class Especialidade
    {
        [Key]
        public long IdEspecialidade { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; } = string.Empty;

        public ICollection<MedicoEspecialidade> MedicoEspecialidades { get; set; } = new List<MedicoEspecialidade>();
    }
}
