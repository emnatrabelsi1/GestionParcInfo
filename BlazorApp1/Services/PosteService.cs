using BlazorApp1.Services.IServices;
using platapp.Domain;

namespace BlazorApp1.Services

{
    public class PosteService : IPosteService
    {
        private readonly HttpClient httpClient;
        public PosteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
           // this.httpClient.BaseAddress = new Uri("https://localhost:7172/api/");
        }
        public async Task<AddPosteRequest> AddPoste(AddPosteRequest Poste)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Poste/add", Poste);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<AddPosteRequest>();
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de l'ajout du Poste. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de l'ajout du Poste : {ex.Message}");
                throw;
            }
        }

        public async Task<List<Poste>> GetPostes()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var users = await httpClient.GetFromJsonAsync<List<Poste>>("https://localhost:7172/api/Poste");
                var usersNonSupprimes = users.Where(p => p.Deleted == false).ToList();

                return usersNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des Postes : {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemovePoste(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7172/api/Poste/Delete/{id}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression du Poste : {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdatePoste(int id, AddPosteRequest etab)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7172/api/Poste/Update/{id}", etab);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la mise à jour du Poste : {ex.Message}");
                throw;
            }
        }
    }
}
