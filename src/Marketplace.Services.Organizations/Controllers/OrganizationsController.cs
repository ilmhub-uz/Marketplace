using AutoMapper;
using Marketplace.Services.Organizations.Entities;
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
	private readonly IMapper _mapper;

	public OrganizationsController(OrganizationManager organizationManager, IMapper mapper)
	{
		_organizationManager = organizationManager;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType(200, Type = typeof(List<Organization>))]
	public async Task<IActionResult> GetOrganizations()
	{
		var organizations = _mapper.Map<List<OrganizationModel>>(_organizationManager.GetOrganizations());

		return Ok(await _organizationManager.GetOrganizations());
	}

	[HttpPost]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> CreateOrganization([FromForm] CreateOrganizationModel organizationModel)
	{
		if (organizationModel == null)
		{
			return BadRequest(ModelState);
		}
		var organization = await _organizationManager.Create(organizationModel);
		return Ok(organization);
	}


	[HttpGet("{id}")]
	[ProducesResponseType(200, Type = typeof(Organization))]
	[ProducesResponseType(400)]
	public async Task<IActionResult> GetById(Guid id)
	{
		if (!await _organizationManager.OrganizationExists(id))
		{
			return NotFound();
		}
		var organization = _mapper.Map<OrganizationModel>(await _organizationManager.GetById(id));

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		return Ok(organization);
	}

	[HttpPut]
	[OrganizationOwner]
	[ProducesResponseType(400)]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromForm] CreateOrganizationModel organizationModel)
	{
		if (organizationModel == null)
		{
			return BadRequest(ModelState);
		}

		if (!await _organizationManager.OrganizationExists(organizationId))
		{
			return NotFound();
		}

		if (!ModelState.IsValid)
		{
			return BadRequest();
		}
		return Ok(await _organizationManager.Update(organizationId, organizationModel));
	}
}