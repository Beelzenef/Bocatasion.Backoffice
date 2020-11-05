﻿using Bocatasion.Backoffice.Interfaces;
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
        HttpClient _httpClient;

        public FoodService(HttpClient http)
        {
            _httpClient = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<List<SandwichModel>> GetAllSandwiches()
        {
            var result = new List<SandwichModel>();
            
            var response = await _httpClient.GetAsync("/SandwichManagement");
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

        public Task<bool> DeleteSandwich(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SandwichModel> GetSandwich(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSandwich(SandwichModel model)
        {
            throw new NotImplementedException();
        }
    }
}