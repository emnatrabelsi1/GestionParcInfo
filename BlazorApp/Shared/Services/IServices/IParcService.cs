using platapp.Domain;

namespace BlazorApp.Shared.Services.IServices
{
    public interface IParcService
    {
        Task<IEnumerable<AddParcRequest>> GetParcs();
        //Task<AddParcRequest> GetParcById(int id);
        Task<IEnumerable<AddUtilisateurRequest>> GetUsersByParc(int id);
        Task<IEnumerable<AddSalleRequest>> GetSallesByParc(int id);
        Task RemoveParc();
        Task<AddParcRequest> AddParc(AddParcRequest parc);
        Task<AddParcRequest> UpdateParc(AddParcRequest parc);
        Task AffectEtablissement(int parcId, int etablissementId);
    }
}
