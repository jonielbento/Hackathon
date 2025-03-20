namespace Hackathon.Api.Entities
{
    public class MedicoEspecialidade
    {
        public long IdMedico { get; set; }
        public Medico Medico { get; set; } = null!;

        public long IdEspecialidade { get; set; }
        public Especialidade Especialidade { get; set; } = null!;
    }
}
