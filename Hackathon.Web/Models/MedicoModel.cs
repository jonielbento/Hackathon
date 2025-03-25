using System.Collections.Generic;

namespace Hackathon.Web.Models
{
    public class MedicoModel
    {
        public long IdMedico { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public List<EspecialidadeModel> Especialidades { get; set; } = new List<EspecialidadeModel>();
        public List<HorarioTrabalhoModel> HorariosTrabalho { get; set; } = new List<HorarioTrabalhoModel>();
    }

    public class EspecialidadeModel
    {
        public long IdEspecialidade { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class HorarioTrabalhoModel
    {
        public long IdHorarioTrabalho { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
    }
}