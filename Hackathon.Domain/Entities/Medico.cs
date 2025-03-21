using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.Entities
{   

    public class Medico
    {
        [Key]
        public long IdMedico { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(9)]
        public string Crm { get; set; } = string.Empty;

        public MedicoLogin? MedicoLogin { get; set; }
        public ICollection<MedicoEspecialidade> MedicoEspecialidades { get; set; } = new List<MedicoEspecialidade>();
        public ICollection<MedicoHorarioTrabalho> HorariosTrabalho { get; set; } = new List<MedicoHorarioTrabalho>();
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
