using platapp.Domain;
using BlazorApp1.Services.IServices;
namespace BlazorApp1.Services
{
    public class AuthService: IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Utilisateur> Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username, Passwd = password });
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