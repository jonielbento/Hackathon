namespace Hackathon.Application.DataTransfers.Requests
{
    public class BuscarMedicosRequest
    {
        public string? Nome { get; set; }
        public long? IdEspecialidade { get; set; }
        public DateTime? DataDisponivel { get; set; }
    }
}