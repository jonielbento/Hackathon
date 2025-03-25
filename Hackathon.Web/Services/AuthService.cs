using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hackathon.Web.Models;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Hackathon.Web.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<string> GetTokenAsync();
        Task<string> GetUserTypeAsync();
        Task<long> GetUserIdAsync();
        Task<bool> IsAuthenticatedAsync();
        Task LogoutAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            string endpoint = request.TipoUsuario == "Medico" 
                ? "api/Auth/medico/login" 
                : "api/Auth/paciente/login";

            var content = new StringContent(
                JsonSerializer.Serialize<object>(request.TipoUsuario == "Medico"
                    ? new { Crm = request.Identificador, Senha = request.Senha }
                    : new { Identificador = request.Identificador, Senha = request.Senha }),
                Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null && loginResponse.Sucesso)
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", loginResponse.Token);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userType", loginResponse.TipoUsuario);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", loginResponse.IdUsuario.ToString());
                }
                return loginResponse ?? new LoginResponse { Sucesso = false, Mensagem = "Erro ao processar resposta" };
            }

            return new LoginResponse { Sucesso = false, Mensagem = "Falha na autenticação" };
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken") ?? string.Empty;
        }

        public async Task<string> GetUserTypeAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userType") ?? string.Empty;
        }

        public async Task<long> GetUserIdAsync()
        {
            var userId = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userId");
            return long.TryParse(userId, out var id) ? id : 0;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userType");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userId");
        }
    }
}