using GasStation.Data.DbContexts;
using GasStation.Data.Interfaces;

namespace GasStation.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public AppDbContext DbContext { get; } = dbContext;

    public IUserRepository Users => new UserRepository(DbContext);

    public IStationRepository Stations => new StationRepository(dbContext);

}
