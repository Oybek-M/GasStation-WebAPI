using FluentValidation;
using GasStation.Application.Common.Exceptions;
using GasStation.Application.Interfaces;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;
using GasStation.Domain.Enums;
using System.Net;

namespace GasStation.Application.Services;

public class AdminService(IUnitOfWork unitOfWork, IValidator<User> validator) : IAdminService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<User> _validator = validator;

    public async Task<List<User>> GetAllAdminsAsync()
    {
        var admins = await _unitOfWork.Users.GetAllAsync();
        admins = admins.Where(p => p.Role == UserRole.Admin).ToList();

        return admins;
    }


    public async Task ChangeUserRoleAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if(user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }
        if (user.Role == UserRole.Admin)
        {
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Not a good job");
        }

        user.Role = user.Role == UserRole.Admin ? UserRole.User : UserRole.Admin;
        await _unitOfWork.Users.UpdateAsync(user);
    }
    
    

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);    
        if(user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        await _unitOfWork.Users.DeleteAsync(user);
    }


    public async Task ChangeStationAsync(int id)
    {
        var station = await _unitOfWork.Stations.GetByIdAsync(id);
        if(station is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Station is not found");
        }

        station.IsActive = station.IsActive == true ? station.IsActive = false : station.IsActive = true;
        await _unitOfWork.Stations.UpdateAsync(station);
    }

    public async Task DeleteStationAsync(int id)
    {
        var station = await _unitOfWork.Stations.GetByIdAsync(id);
        if(station is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Station is not found");
        }

        await _unitOfWork.Stations.DeleteAsync(station);
    }
}
