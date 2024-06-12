using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface IParcService
    {
        Task<List<Parc>> GetParcs();
        //Task<AddParcRequest> GetParcById(int id);
        Task<List<Utilisateur>> GetUsersByParc(int id);
        Task<List<Salle>> GetSallesByParc(int id);
        Task RemoveParc(int id);
        Task<AddParcRequest> AddParc(AddParcRequest parc);
        Task<Parc> AddParcEtab( int idetab);
        Task AffectEtablissement(int parcId, int? etablissementId);
        Task<List<Etablissement>> GetEtablissements();
    }
}
