namespace HouseRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistByIdAsync(string userId);
        Task<bool> UserWithPhoneNumberExistAsync(string phoneNumber);
        Task<bool> UserHasRentsAsync(string userId);
        Task CreateAsync(string userId, string phoneNumber);
    }
}
