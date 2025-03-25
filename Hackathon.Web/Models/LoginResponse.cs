namespace Hackathon.Web.Models
{
    public class LoginResponse
    {
        public bool Sucesso { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public long IdUsuario { get; set; }
        public string TipoUsuario { get; set; } = string.Empty; // "Medico" ou "Paciente"
    }
}