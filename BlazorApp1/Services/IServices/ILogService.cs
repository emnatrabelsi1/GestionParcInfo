using platapp.Domain;

namespace BlazorApp1.Services.IServices
{
    public interface ILogService
    {

        Task<List<Log>> GetLogs();
     
        Task DeleteLog(int id);
    }
}
