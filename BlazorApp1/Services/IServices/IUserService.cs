using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface IUserService
    {
        Task<List<Utilisateur>> GetUtilisateurs();
        //Task<AddUtilisateurRequest> GetUtilisateurById(int id);

        Task<bool> UpdateUtilisateur(int id, AddUtilisateurRequest etab);
        public Task<AddUtilisateurRequest> GetUtilisateurById(int id);
        public Task<bool> RemoveUtilisateur(int id);
        Task<AddUtilisateurRequest> AddUtilisateur(AddUtilisateurRequest Utilisateur);
        
        
        
    }
}
