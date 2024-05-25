using GasStation.Application.DTOs.UserDTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromForm] AddUserDto addUserDto)
    {
        var result = await _accountService.RegisterAsync(addUserDto);
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromForm] LoginDto loginDto)
    {
        var result = await _accountService.LoginAsync(loginDto);
        return Ok(result);
    }

    [HttpPost("sendCode")]
    public async Task<IActionResult> SendCodeAsync([FromForm] string email)
    {
        await _accountService.SendCodeAsync(email);
        return Ok();
    }

    [HttpPost("checkCode")]
    public async Task<IActionResult> CheckCodeAsync([FromForm] string email, [FromForm]string code)
    {
        await _accountService.CheckCodeAsync(email, code);
        return Ok();
    }
}
