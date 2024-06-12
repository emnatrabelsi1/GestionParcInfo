using platapp.Domain;

namespace platapp.ServicesAPI.IServicesAPI
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<Utilisateur> GetAuthenticatedUser();
    }
}
