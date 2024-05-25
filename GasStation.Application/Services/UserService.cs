using GasStation.Application.Common.Exceptions;
using GasStation.Application.DTOs.UserDTOs;
using GasStation.Application.Interfaces;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;
using GasStation.Domain.Enums;
using System.Net;
using System.Net.Http.Headers;

namespace GasStation.Application.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        //User(model) -> UserDto(model)
        var usersModel = new List<UserDto>();
        foreach (var user in users)
        {
            var userDto = (UserDto)user;
            usersModel.Add(userDto);
        }

        return usersModel;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if(user == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        return (UserDto)user;
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        if (user == null) {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        return (UserDto)user;
    }

    public async Task UpdateAsync(int updaterId, int targetId, UpdateUserDto updateUserDto)
    {
        var updaterUser = await _unitOfWork.Users.GetByIdAsync(updaterId);
        if (updaterId != targetId && updaterUser.Role == UserRole.User)
        {
            throw new StatusCodeException(HttpStatusCode.NotAcceptable, "Your role is not accepted to update a user");
        }
        else
        {
            var model = await _unitOfWork.Users.GetByIdAsync(targetId);
            if (model is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
            }


            var user = (User)updateUserDto;
            user.Id = targetId;
            user.Password = model.Password;
            if (model.Email != updateUserDto.Email)
            {
                user.IsVerified = false;
            }

            await _unitOfWork.Users.UpdateAsync(user);
            throw new StatusCodeException(HttpStatusCode.OK, "User has been updated succesfully");
        }
    }

    public async Task DeleteAsync(int deleterId, int targetId)
    {
        var deleterUser = await _unitOfWork.Users.GetByIdAsync(deleterId);
        if(deleterId != targetId && deleterUser.Role == UserRole.User)
        {
            throw new StatusCodeException(HttpStatusCode.NotAcceptable, "Your role is not acceptable to delete a user");
        } else
        {
            var targetUser = await _unitOfWork.Users.GetByIdAsync(targetId);

            if (targetUser is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
            }
            if (targetUser.Role == UserRole.Admin) {
                throw new StatusCodeException(HttpStatusCode.NotAcceptable, "Admins can be deleted only by a special code");
            }

            await _unitOfWork.Users.DeleteAsync(targetUser);
            throw new StatusCodeException(HttpStatusCode.OK, "User has been deleted succesfully");
        }
    }
}
