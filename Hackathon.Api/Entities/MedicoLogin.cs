using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Api.Entities
{
    public class MedicoLogin
    {
        [Key, ForeignKey("Medico")]
        public long IdMedico { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Senha { get; set; } = string.Empty;

        public Medico Medico { get; set; } = null!;
    }
}
