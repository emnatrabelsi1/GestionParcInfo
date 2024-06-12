using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface IAuthService
    {
        public Task<Utilisateur> Login(string username, string password);
        public Task<Utilisateur> GetUser();
    }

}
