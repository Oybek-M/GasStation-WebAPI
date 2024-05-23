using GasStation.Domain.Entities;

namespace GasStation.Application.Interfaces;

public interface IAdminService
{
    Task<List<User>> GetAllAdminsAsync();

    Task ChangeUserRoleAsync(int id);
    Task DeleteUserAsync(int id);

    Task ChangeStationAsync(int id);
    Task DeleteStationAsync(int id);
}
