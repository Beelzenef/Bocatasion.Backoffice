using Bocatasion.Backoffice.Interfaces;
using Bocatasion.Backoffice.Models.Food;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bocatasion.Backoffice.Services.Food
{
    public class FoodService : IFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions options;

        private readonly string ControllerName = "/SandwichManagement/";

        public FoodService(HttpClient http)
        {
            _httpClient = http ?? throw new ArgumentNullException(nameof(http));

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<SandwichModel>> GetAllSandwiches()
        {
            var result = new List<SandwichModel>();

            var response = await _httpClient.GetAsync(ControllerName + "GetAllSandwiches");
            if (response.IsSuccessStatusCode)
            {
                Stream contentStream = await response.Content.ReadAsStreamAsync();
                StreamReader readStream = new StreamReader(contentStream, Encoding.UTF8);
                string responseContent = readStream.ReadToEnd();
                result = JsonSerializer.Deserialize<List<SandwichModel>>(responseContent, options);
            }
            return result;
        }

        public async Task<SandwichModel> CreateSandwich(SandwichCreatableDto model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ControllerName + "CreateSandwich", model);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return null;
        }

        public async Task<bool> DeleteSandwich(int id)
        {
            var response = await _httpClient.DeleteAsync(ControllerName + $"DeleteSandwich/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<SandwichModel> GetSandwich(int id)
        {
            var result = new SandwichModel();

            var response = await _httpClient.GetAsync(ControllerName + $"GetSandwichById/{id}");
            if (response.IsSuccessStatusCode)
            {
                Stream contentStream = await response.Content.ReadAsStreamAsync();
                StreamReader readStream = new StreamReader(contentStream, Encoding.UTF8);
                string responseContent = readStream.ReadToEnd();
                result = JsonSerializer.Deserialize<SandwichModel>(responseContent, options);
            }
            return result;
        }

        public async Task<bool> UpdateSandwich(SandwichModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(ControllerName + "UpdateSandwich", model);
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return false;
        }
    }
}
