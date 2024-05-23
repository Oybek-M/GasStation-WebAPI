using GasStation.Domain.Entities;

namespace GasStation.Application.DTOs.UserDTOs;

public class UserDto : AddUserDto
{
    public int Id { get; set; }


    public static implicit operator UserDto(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            CarNumber = user.CarNumber,
        };
    }
}
