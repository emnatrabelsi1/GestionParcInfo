using BlazorApp1.Services.IServices;
using platapp.Domain;
using System.Net.Http;

namespace BlazorApp1.Services
{
    public class SalleService : ISalleService
    {

        private readonly HttpClient httpClient;
        public SalleService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
         //   this.httpClient.BaseAddress = new Uri("https://localhost:7172/api/");
        }
        public async Task<AddSalleRequest> AddSalle(AddSalleRequest Salle)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Salle/add", Salle);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<AddSalleRequest>();
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de l'ajout de la salle. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de l'ajout de la salle : {ex.Message}");
                throw;
            }
        }

        public Task<AddSalleRequest> GetSalleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Salle>> GetSalles()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var users = await httpClient.GetFromJsonAsync<List<Salle>>("https://localhost:7172/api/Salle");
                var usersNonSupprimes = users.Where(p => p.Deleted == false).ToList();

                return usersNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des parcs : {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveSalle(int id)
        {

            try
            {
                HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7172/api/Salle/Delete/{id}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression de l'utilisateur : {ex.Message}");
                throw;
            }
        }

        public Task<bool> UpdateSalle(int id, AddSalleRequest etab)
        {
            throw new NotImplementedException();
        }
    }
}
