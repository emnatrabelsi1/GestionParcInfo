using BlazorApp1.Services.IServices;
using platapp.Domain;
using platapp;
using System.Net;

namespace BlazorApp1.Services
{
    public class LogService : ILogService
    {
        private readonly HttpClient httpClient;

        public LogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
            //  this.httpClient.BaseAddress = new Uri("https://localhost:7172/");
        }

        public async Task DeleteLog(int id)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête PUT à l'API pour supprimer un établissement
                HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7172/api/Log/Delete/{id}", null);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Log supprimé avec succès.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Le Log avec l'ID {id} n'existe pas.");
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de la suppression du log. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression du log: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Log>> GetLogs()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var logs = await httpClient.GetFromJsonAsync<List<Log>>("https://localhost:7172/api/Log");
                var logsNonSupprimes = logs.Where(p => p.Deleted == false).ToList();

                return logsNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des parcs : {ex.Message}");
                throw;
            }
        }
    }
}
