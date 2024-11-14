using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Models;

namespace System.Linq
{
    public static class IQueryableHouseExtension
    {
        public static IQueryable<HouseServiceModel> ProjectToHouseServiceModel(this IQueryable<House> houses)
            => houses.Select(h => new HouseServiceModel
            {
                Id = h.Id,
                Title = h.Title,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
                PricePerMonth = h.PricePerMonth,
                Address = h.Address,
                IsRented = h.RenterId != null
            });
    }
}
