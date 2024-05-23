namespace GasStation.Data.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IStationRepository Stations { get; }
}
