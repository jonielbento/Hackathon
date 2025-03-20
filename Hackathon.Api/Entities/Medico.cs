using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.Api.Entities
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
