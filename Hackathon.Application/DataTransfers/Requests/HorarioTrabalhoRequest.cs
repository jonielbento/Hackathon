using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Application.DataTransfers.Requests
{
    public class HorarioTrabalhoRequest
    {
        [Required(ErrorMessage = "A data do trabalho é obrigatória")]
        public DateTime DiaTrabalho { get; set; }

        [Required(ErrorMessage = "A hora de início é obrigatória")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "A hora de fim é obrigatória")]
        public TimeSpan HoraFim { get; set; }
    }
}