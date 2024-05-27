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

    [HttpPost("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeUserRoleAsync(int id)
    {
        await _adminService.ChangeUserRoleAsync(id);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserAsync(int targetId, [FromForm]UpdateUserDto updateUserDto)
    {
        var updaterId = int.Parse(HttpContext.User.FindFirst("Id").Value);

        await _userService.UpdateAsync(updaterId, targetId, updateUserDto);
        return Ok();
    }
    
    [HttpDelete("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        await _adminService.DeleteUserAsync(id);
        return Ok();
    }

    [HttpPost("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStationAsync(int id)
    {
        await _adminService.ChangeStationAsync(id);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public Task<IActionResult> UpdateStationAsync()
    {
        
        return Ok();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStationAsync(int id)
    {
        await _adminService.DeleteStationAsync(id);
        return Ok();
    }
}
