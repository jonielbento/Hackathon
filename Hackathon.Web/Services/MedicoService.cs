using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hackathon.Web.Models;

namespace Hackathon.Web.Services
{
    public interface IMedicoService
    {
        Task<List<MedicoModel>> GetAllMedicosAsync();
        Task<MedicoModel?> GetMedicoByIdAsync(long id);
        Task<List<MedicoModel>> BuscarMedicosAsync(BuscarMedicosRequest request);
    }

    public class MedicoService : IMedicoService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public MedicoService(HttpClient httpClient, IAuthService authService)
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

        public async Task<List<MedicoModel>> GetAllMedicosAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetFromJsonAsync<List<MedicoModel>>("api/Medico");
            return response ?? new List<MedicoModel>();
        }

        public async Task<MedicoModel?> GetMedicoByIdAsync(long id)
        {
            await SetAuthHeaderAsync();
            return await _httpClient.GetFromJsonAsync<MedicoModel>($"api/Medico/{id}");
        }
        
        public async Task<List<MedicoModel>> BuscarMedicosAsync(BuscarMedicosRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Medico/buscar", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<MedicoModel>>();
            return result ?? new List<MedicoModel>();
        }
    }
}