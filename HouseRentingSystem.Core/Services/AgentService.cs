using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;
        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Agent
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            });

            await repository.SaveChangesAsync();
        }
        public async Task<bool> ExistByIdAsync(string agentId)
        {
            return await repository
                .AllReadOnly<Agent>()
                .AnyAsync(a => a.UserId == agentId);
        }
        public async Task<bool> UserHasRentsAsync(string userId)
        {
            return await repository
                .AllReadOnly<House>()
                .AnyAsync(h => h.RenterId == userId);


        }
        public async Task<bool> UserWithPhoneNumberExistAsync(string phoneNumber)
        {
            return await repository
                .AllReadOnly<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
