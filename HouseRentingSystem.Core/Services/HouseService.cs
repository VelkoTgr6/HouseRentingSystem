using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly IRepository repository;
        public HouseService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<HouseQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 3)
        {
            var housesQuery = repository.AllReadOnly<House>();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = housesQuery.Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string searchTermToLower = searchTerm.ToLower();

                housesQuery = housesQuery.Where(h => h.Title.ToLower().Contains(searchTermToLower) || 
                                               h.Description.ToLower().Contains(searchTermToLower) ||
                                               h.Address.ToLower().Contains(searchTermToLower));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery
                    .OrderByDescending(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h=> h.Id),
                _ => housesQuery
                    .OrderByDescending(h => h.Id)
            };

            var houses = await housesQuery
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                })
                .ToListAsync();

            var totalHousesCount = await housesQuery.CountAsync();

            return new HouseQueryServiceModel
            {
                TotalHousesCount = totalHousesCount,
                Houses = houses
            };
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(h => new HouseCategoryServiceModel
                {
                    Id = h.Id,
                    Name = h.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> CategoryExistAsync(int categoryId)
        {
            return await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateAsync(HouseFormModel model,int agentId)
        {
            var house = new House()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Address = model.Address,
                CategoryId = model.CategoryId,
                AgentId = agentId
            };

            await repository.AddAsync(house);
            await repository.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
        {
            return await repository.AllReadOnly<House>()
                .OrderByDescending(h => h.Id)
                .Take(3)
                .Select(h => new HouseIndexServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();
        }
    }
}
