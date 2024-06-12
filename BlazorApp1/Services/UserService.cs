using BlazorApp1.Pages;
using BlazorApp1.Services.IServices;
using platapp.Domain;
using System.Net.Http;

namespace BlazorApp1.Services
{
    public class UserService : IUserService
    {

       
        private readonly HttpClient httpClient;
        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
          //  this.httpClient.BaseAddress = new Uri("https://localhost:7172/api/");
        }
        public async Task<AddUtilisateurRequest> AddUtilisateur(AddUtilisateurRequest Utilisateur)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Utilisateur/add", Utilisateur);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<AddUtilisateurRequest>();
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de l'ajout du parc. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de l'ajout du parc : {ex.Message}");
                throw;
            }
        }
    

        public async Task<List<Utilisateur>> GetUtilisateurs()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var users = await httpClient.GetFromJsonAsync<List<Utilisateur>>("https://localhost:7172/api/Utilisateur");
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

        public async Task<bool> RemoveUtilisateur(int id)
       
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7172/api/Utilisateur/Delete/{id}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression de l'utilisateur : {ex.Message}");
                throw;
            }
        }



        public async Task<bool> UpdateUtilisateur(int id, AddUtilisateurRequest etab)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7172/api/Utilisateur/Update/{id}", etab);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la mise à jour de l'utilisateur : {ex.Message}");
                throw;
            }
        }
        public async Task<AddUtilisateurRequest> GetUtilisateurById(int id)
        {
            var response = await httpClient.GetAsync($"https://localhost:7172/api/Utilisateur/GetUtilisateurById/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AddUtilisateurRequest>();
            }
            else
            {
                throw new HttpRequestException($"Failed to get utilisateur. Status code: {response.StatusCode}");
            }
        }


    }
}
