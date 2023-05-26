using Identity.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Identity.Core.Managers;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager _userManager;
    private ILogger<AccountController> _logger;

    public AccountController(UserManager userManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel createUserModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.Register(createUserModel);

        return Ok(new UserModel(user));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _userManager.Login(loginUserModel);

        return Ok(new { Token = token });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var user = await _userManager.GetUser(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(new UserModel(user));
    }
}