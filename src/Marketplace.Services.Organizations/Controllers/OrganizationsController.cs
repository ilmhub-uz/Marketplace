using Marketplace.Services.Organizations.Managers;
using Marketplace.Services.Organizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.Services.Organizations.Filters.OrganizationOwnerFilterAttribute;

namespace Marketplace.Services.Organizations.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrganizationsController : ControllerBase
{
	/*
		POST   api/organizations
		GET    api/organizations
		GET    api/organizations/{id}
		UPDATE api/organizations/{id}
		POST   api/organizations/{id}/users
		GET    api/organizations/{id}/users/{userId}
	 */

	private readonly OrganizationManager _organizationManager;

	public OrganizationsController(OrganizationManager organizationManager)
	{
		_organizationManager = organizationManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetOrganizations()
	{
		return Ok(await _organizationManager.GetOrganizations());
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrganization([FromForm] CreateOrganizationModel organizationModel)
	{
		return Ok(await _organizationManager.Create(organizationModel));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		return Ok(await _organizationManager.GetById(id));
	}

	[HttpPut]
	[OrganizationOwner]
	public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromForm] CreateOrganizationModel organizationModel)
	{
		return Ok(await _organizationManager.Update(organizationId, organizationModel));
	}
}