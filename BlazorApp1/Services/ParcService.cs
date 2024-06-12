

using platapp.Domain;
using System.Net.Http.Json;
using BlazorApp1.Services.IServices;
using BlazorApp1.Pages;

namespace BlazorApp1.Services
{
    public class ParcService : IParcService
    {
        private readonly HttpClient httpClient;

        public ParcService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
           // this.httpClient.BaseAddress = new Uri("https://localhost:7172/api/");
        }
        public async Task<AddParcRequest> AddParc(AddParcRequest parc)
        {

            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Parc/add", parc);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<AddParcRequest>();
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

        public async Task AffectEtablissement(int parcId, int? etablissementId)
        {
            try
            {
                // Créer un objet représentant les données à envoyer dans la requête
                var data = new { ParcId = parcId, EtablissementId = etablissementId };

                // Utilisation de HttpClient pour envoyer une requête PUT à l'API pour affecter un établissement à un parc
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7172/api/Parc/AffecterEtablissement/{parcId}/{etablissementId}", data);

                // Vérifier si la requête a réussi
                if (!response.IsSuccessStatusCode)
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de l'affectation de l'établissement au parc. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de l'affectation de l'établissement au parc : {ex.Message}");
                throw;
            }
        }

        

        public async Task<List<platapp.Domain.Parc>> GetParcs()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var parcs = await httpClient.GetFromJsonAsync<List<platapp.Domain.Parc>>("https://localhost:7172/api/Parc");
                var parcsNonSupprimes = parcs.Where(p => p.Deleted == false).ToList();

                return parcsNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des parcs : {ex.Message}");
                throw;
            }
        }


        public async Task<List<Salle>> GetSallesByParc(int id)
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les salles par parc depuis l'API
                var salles = await httpClient.GetFromJsonAsync<List<Salle>>($"https://localhost:7172/api/Parc/GetSallesDuParc/{id}");
                var sallesNonSupprimes = salles.Where(p => p.Deleted == false).ToList();

                return sallesNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des salles : {ex.Message}");
                throw;
            }
        }

        public async Task<List<Utilisateur>> GetUsersByParc(int id)
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les salles par parc depuis l'API
                var Users = await httpClient.GetFromJsonAsync<List<Utilisateur>>($"https://localhost:7172/api/Parc/GetUsersDuParc/{id}");
                var usersNonSupprimes = Users.Where(p => p.Deleted == false).ToList();

                return usersNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des salles : {ex.Message}");
                throw;
            }
        }

        public async Task RemoveParc(int id)
        {
            try
            {

                var parcs = await httpClient.GetFromJsonAsync<List<platapp.Domain.Parc>>("https://localhost:7172/api/Parc");
                // Appeler l'API pour supprimer le parc avec l'identifiant donné
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7172/api/Parc/Delete/{id}",parcs);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // Mettre à jour la liste des parcs après la suppression
                    parcs = await GetParcs();
                }
                else
                {
                    // Gérer l'échec de la suppression
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de la suppression du parc. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression du parc : {ex.Message}");
                throw;
            }
        }




       

        public async Task<List<Etablissement>> GetEtablissements()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les établissements depuis l'API
                var etablissements = await httpClient.GetFromJsonAsync<List<Etablissement>>("https://localhost:7172/api/Etablissement");
                return etablissements;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des établissements : {ex.Message}");
                throw;
            }
        }

        public async Task<platapp.Domain.Parc> AddParcEtab( int idetab)
        {
            platapp.Domain.Parc parc = new platapp.Domain.Parc () ;
            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"https://localhost:7172/api/Parc/addParcToEtab/{idetab}", parc);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<platapp.Domain.Parc>();
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



    }
}
