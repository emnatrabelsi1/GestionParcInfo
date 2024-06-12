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
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Utilisateur>> GetAllUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<Utilisateur>>("api/Utilisateur");
        }
    }
}
