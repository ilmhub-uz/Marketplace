using Marketplace.Services.Identity.Managers;
using Marketplace.Services.Identity.Models;
using Marketplace.Services.Identity.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Identity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;
    private readonly UserProvider _userProvider;

    public UserController(UserManager userManager, UserProvider userProvider)
    {
        _userManager = userManager;
        _userProvider = userProvider;
    }


    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserModel model)
    {
        var userId = _userProvider.UserId;

        var user = await _userManager.UpdateProfileAsync(userId, model);

        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(new UserModel(user));
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = _userProvider.UserId;

        var user = await _userManager.GetUser(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(new UserModel(user));
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUser(string userName)
    {
        var user = await _userManager.GetUser(userName);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(new UserModel(user));
    }

    [HttpGet("get_all_users")]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userManager.GetAllUserAsync();

        return Ok(users);
    }
}