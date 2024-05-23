using FluentValidation;
using GasStation.Application.Common.Exceptions;
using GasStation.Application.Common.Security;
using GasStation.Application.Common.Validators;
using GasStation.Application.DTOs.UserDTOs;
using GasStation.Application.Interfaces;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace GasStation.Application.Services;

public class AccountService(IUnitOfWork unitOfWork,
                            AuthManager authManager,
                            IValidator<User> validator,
                            IMemoryCache memoryCache,
                            IEmailService emailService) : IAccountService
{

    public AuthManager auth = authManager;

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<User> _validator = validator;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IEmailService _emailService = emailService;


    public async Task<bool> RegisterAsync(AddUserDto addUserDto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(addUserDto.Email);
        if (user is not null)
        {
            throw new StatusCodeException(HttpStatusCode.AlreadyReported, "User is already exists");
        }

        var result = await _validator.ValidateAsync(user);
        if (!result.IsValid)
        {
            throw new ValidationException(result.GetErrorMessage());
        }

        var entity = (User)addUserDto;
        entity.Password = PasswordHasher.GetHash(entity.Password);

        await _unitOfWork.Users.CreateAsync(entity);
        return true;
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
        if (user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        if(!user.Password.Equals(PasswordHasher.GetHash(loginDto.Password)))
        {
            throw new StatusCodeException(HttpStatusCode.Conflict, "Password is incorrect");
        }
        if (!user.IsVerified)
        {
            throw new StatusCodeException(HttpStatusCode.BadRequest, "User is not verified");
        }

        return auth.GenerateToken(user);
    }



    public async Task SendCodeAsync(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        if (user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        var code = GenerateCode();
        _memoryCache.Set(email, code, TimeSpan.FromSeconds(100));

        await _emailService.SendMessageAsync(email, "Verification code", code);
    }

    public async Task<bool> CheckCodeAsync(string email, string code)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        if (user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User is not found");
        }

        if (_memoryCache.TryGetValue(email, out var result))
        {
            if (code.Equals(result))
            {
                user.IsVerified = true;
                await _unitOfWork.Users.UpdateAsync(user);

                return true;
            } else
            {
                throw new StatusCodeException(HttpStatusCode.Conflict, "Code is incorrect");
            }
        } else
        {
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Code is expired");
        }
    }

    private string GenerateCode()
    {
        var code = new Random().Next(10000, 99999).ToString();
        return code;
    }
}
