using System;

namespace Hackathon.Web.Models
{
    public class BuscarMedicosRequest
    {
        public string? Nome { get; set; }
        public long? IdEspecialidade { get; set; }
        public DateTime? DataDisponivel { get; set; }
    }
}