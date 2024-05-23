using GasStation.Domain.Entities;


namespace GasStation.Application.DTOs.UserDTOs;

public class AddUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CarNumber { get; set; } = string.Empty;



    public static implicit operator User(AddUserDto userDto)
    {
        return new User()
        {
            FullName = userDto.FullName,
            Email = userDto.Email,
            Password = userDto.Password,
            CarNumber = userDto.CarNumber,
        };
    }
}
