using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hackathon.Web.Models;

namespace Hackathon.Web.Services
{
    public interface IConsultaService
    {
        Task<List<ConsultaModel>> GetConsultasByMedicoAsync(long medicoId);
        Task<List<ConsultaModel>> GetConsultasByPacienteAsync(long pacienteId);
        Task<ConsultaModel?> GetConsultaByIdAsync(long id);
        Task<ConsultaModel?> AgendarConsultaAsync(AgendarConsultaRequest request);
    }

    public class ConsultaService : IConsultaService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public ConsultaService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<ConsultaModel>> GetConsultasByMedicoAsync(long medicoId)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetFromJsonAsync<List<ConsultaModel>>($"api/Consulta/medico/{medicoId}");
            return response ?? new List<ConsultaModel>();
        }

        public async Task<List<ConsultaModel>> GetConsultasByPacienteAsync(long pacienteId)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetFromJsonAsync<List<ConsultaModel>>($"api/Consulta/paciente/{pacienteId}");
            return response ?? new List<ConsultaModel>();
        }

        public async Task<ConsultaModel?> GetConsultaByIdAsync(long id)
        {
            await SetAuthHeaderAsync();
            return await _httpClient.GetFromJsonAsync<ConsultaModel>($"api/Consulta/{id}");
        }

        public async Task<ConsultaModel?> AgendarConsultaAsync(AgendarConsultaRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Consulta", request);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ConsultaModel>();
            }
            
            return null;
        }
    }
}