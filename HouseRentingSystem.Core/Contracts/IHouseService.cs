using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();
        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();
        Task<bool> CategoryExistAsync(int categoryId);
        Task<int> CreateAsync(HouseFormModel model, int agentId);
        Task<HouseQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 3);
        Task<IEnumerable<string>> AllCategoriesNamesAsync();
        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);
        Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId);
        Task<bool> ExistAsync(int id);
        Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int houseId);
        Task EditAsync(int houseId, HouseFormModel model);
        Task<bool> HasAgentWithId(int houseId, string userId);
        Task<HouseFormModel?> GetHouseFormModelByIdAsync(int houseId);
    }
}
