using System;
using System.Collections.Generic;

namespace Hackathon.Application.DataTransfers.Responses
{
    public class MedicoResponse
    {
        public long IdMedico { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public List<EspecialidadeResponse> Especialidades { get; set; } = new List<EspecialidadeResponse>();
        public List<HorarioTrabalhoResponse> HorariosTrabalho { get; set; } = new List<HorarioTrabalhoResponse>();
    }

    public class EspecialidadeResponse
    {
        public long IdEspecialidade { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class HorarioTrabalhoResponse
    {
        public long IdMedicoHorarioTrabalho { get; set; }
        public DateTime DiaTrabalho { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}