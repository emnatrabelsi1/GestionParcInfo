using BlazorApp1.Pages;
using BlazorApp1.Services.IServices;
using platapp.Domain;
using System.Net;

namespace BlazorApp1.Services
{
    public class EtablissementService : IEtablissementService
    {

        private readonly HttpClient httpClient;

        public EtablissementService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Mettez à jour l'URL de base pour utiliser HTTPS
          //  this.httpClient.BaseAddress = new Uri("https://localhost:7172/");
        }


        public async Task<AddEtablissementRequest> AddEtablissement(AddEtablissementRequest Etablissement)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête POST à l'API pour ajouter un parc
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Etablissement/add", Etablissement);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // L'ajout du parc a réussi, retourner la représentation de la demande de parc ajoutée
                    return await response.Content.ReadFromJsonAsync<AddEtablissementRequest>();
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
                Console.WriteLine($"Erreur lors de l'ajout de l'etablissement : {ex.Message}");
                throw;
            }
        
    }

        public async Task<List<Etablissement>> GetEtablissements()
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les parcs depuis l'API
                var etablissements = await httpClient.GetFromJsonAsync<List<Etablissement>>("https://localhost:7172/api/Etablissement");
                var etablissementsNonSupprimes = etablissements.Where(p => p.Deleted == false).ToList();

                return etablissementsNonSupprimes;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des parcs : {ex.Message}");
                throw;
            }
        }

        public async Task DeleteEtablissement(int id)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête PUT à l'API pour supprimer un établissement
                HttpResponseMessage response = await httpClient.PutAsync($"https://localhost:7172/api/Etablissement/Delete/{id}", null);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Établissement supprimé avec succès.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"L'établissement avec l'ID {id} n'existe pas.");
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de la suppression de l'établissement. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la suppression de l'établissement : {ex.Message}");
                throw;
            }
        }


        public async Task<AddEtablissementRequest> UpdateEtablissement(int id, AddEtablissementRequest etablissement)
        {
            try
            {
                // Utilisation de HttpClient pour envoyer une requête PUT à l'API pour mettre à jour un établissement
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7172/api/Etablissement/Update/{id}", etablissement);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    // La mise à jour de l'établissement a réussi, retourner la représentation de la demande d'établissement mise à jour
                    return await response.Content.ReadFromJsonAsync<AddEtablissementRequest>();
                }
                else
                {
                    // La requête a échoué, lire le message d'erreur de la réponse
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Échec de la mise à jour de l'établissement. Code d'état HTTP : {response.StatusCode}, Message : {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de manière appropriée
                Console.WriteLine($"Erreur lors de la mise à jour de l'établissement : {ex.Message}");
                throw;
            }
        }


        public async Task<List<platapp.Domain.Parc>> GetParcsDeEtab(int id)
        {
            try
            {
                // Utilisation de HttpClient pour récupérer les salles par parc depuis l'API
                var salles = await httpClient.GetFromJsonAsync<List<platapp.Domain.Parc>>($"https://localhost:7172/api/Etablissement/GetParcsDeEtab/{id}");
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

       
    }
}
