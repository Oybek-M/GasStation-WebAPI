using GasStation.Data.DbContexts;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Data.Repositories;

public class UserRepository(AppDbContext dbContext)
    : GenericRepository<User>(dbContext), IUserRepository
{
    public AppDbContext DbContext { get; } = dbContext;

    public async Task<User> GetByEmailAsync(string email)
    {
        var res = await DbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        return res;
    }
}
