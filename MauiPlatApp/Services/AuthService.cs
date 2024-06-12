using platapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MauiPlatApp.Services.IServices;

namespace MauiPlatApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Utilisateur> Login(string username, string password)
        {
            var loginUser = new LoginUser { username = username, Passwd = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginUser);
            if (response.IsSuccessStatusCode)
            {
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                return loginResult.User;
            }

            return null;
        }

        public async Task<Utilisateur> GetUser()
        {
            // Implémentez la logique pour obtenir l'utilisateur actuellement connecté
            return null;
        }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public Utilisateur User { get; set; }
    }
}