using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface IEtablissementService
    {
          Task<List<Etablissement>> GetEtablissements();
        //Task<AddEtablissementRequest> GetEtablissementById(int id);
        Task<List<Parc>> GetParcsDeEtab(int id);
        Task DeleteEtablissement(int id);
        Task<AddEtablissementRequest> AddEtablissement(AddEtablissementRequest Etablissement);
        Task<AddEtablissementRequest> UpdateEtablissement(int id, AddEtablissementRequest etablissement);



    }
}
