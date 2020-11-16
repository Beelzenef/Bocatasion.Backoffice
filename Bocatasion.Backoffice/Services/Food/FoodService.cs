using Bocatasion.Backoffice.Interfaces;
using Bocatasion.Backoffice.Models.Food;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bocatasion.Backoffice.Services.Food
{
    public class FoodService : IFoodService
    {
        private readonly HttpClient _httpClient;

        private readonly string ControllerName = "/SandwichManagement/";

        public FoodService(HttpClient http)
        {
            _httpClient = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<List<SandwichModel>> GetAllSandwiches()
        {
            var result = new List<SandwichModel>();
            
            var response = await _httpClient.GetAsync(ControllerName + "GetAllSandwiches");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<SandwichModel>>(responseContent);
            }
            return result;
        }

        public Task<SandwichModel> CreateSandwich(SandwichModel model)
        {
            throw new NotImplementedException();
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
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<SandwichModel>(responseContent);
            }
            return result;
        }

        public Task<bool> UpdateSandwich(SandwichModel model)
        {
            throw new NotImplementedException();
        }
    }
}
