using Marketplace.Services.Identity.Managers;
using Marketplace.Services.Identity.Models;
using Marketplace.Services.Identity.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Identity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountManager _accountManager;


    public AccountController(AccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel createUserModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _accountManager.Register(createUserModel);

        return Ok(new UserModel(user));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _accountManager.Login(loginUserModel);

        return Ok(new { Token = token });
    }

  
}