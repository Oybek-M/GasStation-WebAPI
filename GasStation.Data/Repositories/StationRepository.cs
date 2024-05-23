using GasStation.Data.DbContexts;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Data.Repositories;

public class StationRepository(AppDbContext dbContext)
    : GenericRepository<Station>(dbContext), IStationRepository
{
}
