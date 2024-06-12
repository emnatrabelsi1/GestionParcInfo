using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface IPosteService
    {
        Task<List<Poste>> GetPostes();
        //Task<AddPosteRequest> GetPosteById(int id);
     
        Task<bool> RemovePoste(int id);
        Task<AddPosteRequest> AddPoste(AddPosteRequest Poste);
        Task<bool> UpdatePoste(int id, AddPosteRequest etab);


    }
}
