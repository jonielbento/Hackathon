using System.ComponentModel.DataAnnotations;

namespace Hackathon.Api.Entities
{
    public class MedicoHorarioTrabalho
    {
        [Key]
        public long IdMedicoHorarioTrabalho { get; set; }

        public long IdMedico { get; set; }
        public Medico Medico { get; set; } = null!;

        [Required]
        public DateTime DiaTrabalho { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFim { get; set; }
    }
}
