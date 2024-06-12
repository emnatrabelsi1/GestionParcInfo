using platapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Services.IServices;

namespace WpfApp3.Services
{
    public class ParcService : IParcService
    {
        private readonly HttpClient _httpClient;

        public ParcService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Parc>> GetAllParcs()
        {
            return await _httpClient.GetFromJsonAsync<List<Parc>>("api/Parc");
        }
    }
}