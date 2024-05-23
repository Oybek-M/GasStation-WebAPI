using GasStation.Domain.Entities;


namespace GasStation.Application.Interfaces;

public interface IAuthManager
{
    string GenerateToken(User user);
}
