using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface ISalleService
    {
        Task<List<Salle>> GetSalles();
        //Task<AddSalleRequest> GetSalleById(int id);

        Task<bool> UpdateSalle(int id, AddSalleRequest etab);
        public Task<AddSalleRequest> GetSalleById(int id);
        public Task<bool> RemoveSalle(int id);
        Task<AddSalleRequest> AddSalle(AddSalleRequest Salle);
    }
}
