using GasStation.Domain.Enums;

namespace GasStation.Domain.Entities;

public class User : Base
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public string CarNumber { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    public int X { get; set; }
    public int Y { get; set; }
}
