

using platapp.Domain;
using System.Net.Http.Json;
using BlazorApp.Shared.Services.IServices;

namespace BlazorApp.Shared.Services
{
    public class ParcService : IParcService
    {
        private readonly HttpClient httpClient;

        public ParcService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    
public Task<AddParcRequest> AddParc(AddParcRequest parc)
        {
            throw new NotImplementedException();
        }

        public Task AffectEtablissement(int parcId, int etablissementId)
        {
            throw new NotImplementedException();
        }

     /*   public async Task<AddParcRequest> GetParcById(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Parc/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(AddParcRequest);
                    }

                    return await response.Content.ReadFromJsonAsync<AddParcRequest>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }*/

        public async Task<IEnumerable<AddParcRequest>> GetParcs()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Parc");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<AddParcRequest>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<AddParcRequest>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<AddSalleRequest>> GetSallesByParc(int id)
        {
            try
            {
                var response = await this.httpClient.GetAsync($"api/Parc/GetSallesDuParc/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<AddSalleRequest>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<AddSalleRequest>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<AddUtilisateurRequest>> GetUsersByParc(int id)
        {
            try
            {
                var response = await this.httpClient.GetAsync($"api/Parc/GetUsersDuParc/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<AddUtilisateurRequest>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<AddUtilisateurRequest>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public Task RemoveParc()
        {
            throw new NotImplementedException();
        }

        public Task<AddParcRequest> UpdateParc(AddParcRequest parc)
        {
            throw new NotImplementedException();
        }
    }
}
