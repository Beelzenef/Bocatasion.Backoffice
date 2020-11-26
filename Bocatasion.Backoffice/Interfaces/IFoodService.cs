using Bocatasion.Backoffice.Models.Food;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bocatasion.Backoffice.Interfaces
{
    public interface IFoodService
    {
        Task<List<SandwichModel>> GetAllSandwiches();
        Task<SandwichModel> GetSandwich(int id);
        Task<bool> DeleteSandwich(int id);
        Task<SandwichModel> CreateSandwich(SandwichCreatableDto model);
        Task<bool> UpdateSandwich(SandwichModel model);
    }
}
