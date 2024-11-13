namespace HouseRentingSystem.Core.Models.House
{
    public class HouseQueryServiceModel
    {
        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
        public int TotalHousesCount { get; set; }
    }
}
