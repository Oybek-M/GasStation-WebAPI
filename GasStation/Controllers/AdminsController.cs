using GasStation.Application.DTOs.UserDTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace GasStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController(IAdminService adminService, IUserService userService) : ControllerBase
{
    private readonly IAdminService _adminService = adminService;
    private readonly IUserService _userService = userService;

    [HttpGet("Admins")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAdminsAsync()
    {
        var admins = await _adminService.GetAllAdminsAsync();
        return Ok(admins);
    }

    // Qolgan qismini tugatish kk
}
