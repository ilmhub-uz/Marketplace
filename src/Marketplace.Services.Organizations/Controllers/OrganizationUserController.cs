using Marketplace.Services.Organizations.Entities;
using Marketplace.Services.Organizations.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Organizations.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationUserController : ControllerBase
{
    private readonly OrganizationUserManager _organizationUserManager;

    public OrganizationUserController(OrganizationUserManager organizationUserManager)
    {
        _organizationUserManager = organizationUserManager;
    }


    [HttpGet("getUser")]
    public async Task<IActionResult> GetOrganizationUser(Guid userId, Guid organizationId)
    {
        var organizationUser = await _organizationUserManager.GetOrganizationUser(organizationId, userId)!;
        if (organizationUser == null) return NotFound();
        return Ok(organizationUser);
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> AddUser(Guid userId, Guid organizationId)
    {
        return Ok(await _organizationUserManager.AddUser(userId, organizationId));
    }

    [HttpGet("getUsers")]
    public async Task<IActionResult> GetOrganizationUser(Guid organizationId)
    {
        return Ok(await _organizationUserManager.GetOrganizationUsers(organizationId));
    }





}