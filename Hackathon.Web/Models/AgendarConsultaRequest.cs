using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Web.Models
{
    public class AgendarConsultaRequest
    {
        [Required(ErrorMessage = "O médico é obrigatório")]
        public long IdMedico { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório")]
        public long IdPaciente { get; set; }

        [Required(ErrorMessage = "A data e hora de início da consulta são obrigatórias")]
        public DateTime DataConsultaInicio { get; set; }

        [Required(ErrorMessage = "A data e hora de fim da consulta são obrigatórias")]
        public DateTime DataConsultaFim { get; set; }

        [Required(ErrorMessage = "O valor da consulta é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da consulta deve ser maior que zero")]
        public decimal ValorConsulta { get; set; }
    }
}