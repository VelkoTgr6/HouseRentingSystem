using HouseRentingSystem.Core.Models.Home;

namespace HouseRentingSystem.Core.Models.House
{
    public class AllHousesQueryModel
    {
        public int TotalHousesCount { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}
