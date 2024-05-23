using GasStation.Domain.Entities;

namespace GasStation.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetByEmailAsync(string email);
}
