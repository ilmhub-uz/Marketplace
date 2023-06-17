using Marketplace.Services.Organizations.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.Services.Organizations.Filters.OrganizationOwnerFilterAttribute;

namespace Marketplace.Services.Organizations.Controllers;

[Route("api/organizations/{organizationId}/users")]
[ApiController]
[Authorize]
[OrganizationOwner]
public class OrganizationUserController : ControllerBase
{
	private readonly OrganizationUserManager _organizationUserManager;

	public OrganizationUserController(OrganizationUserManager organizationUserManager)
	{
		_organizationUserManager = organizationUserManager;
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetOrganizationUser(Guid userId, Guid organizationId)
	{
		var organizationUser = await _organizationUserManager.GetOrganizationUser(organizationId, userId)!;
		if (organizationUser == null) return NotFound();
		return Ok(organizationUser);
	}

	[HttpPost("{userId}")]
	public async Task<IActionResult> AddUser(Guid userId, Guid organizationId)
	{
		return Ok(await _organizationUserManager.AddUser(userId, organizationId));
	}

	[HttpGet]
	public async Task<IActionResult> GetOrganizationUser(Guid organizationId)
	{
		return Ok(await _organizationUserManager.GetOrganizationUsers(organizationId));
	}
}