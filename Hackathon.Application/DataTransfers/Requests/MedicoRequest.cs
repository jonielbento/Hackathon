using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Application.DataTransfers.Requests
{
    public class MedicoRequest
    {
        [Required(ErrorMessage = "O nome do médico é obrigatório")]
        [MaxLength(255, ErrorMessage = "O nome não pode exceder 255 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CRM é obrigatório")]
        [MaxLength(9, ErrorMessage = "O CRM não pode exceder 9 caracteres")]
        public string Crm { get; set; } = string.Empty;

        public List<long> EspecialidadesIds { get; set; } = new List<long>();
    }
}