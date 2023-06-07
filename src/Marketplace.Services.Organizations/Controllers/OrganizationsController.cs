using Marketplace.Services.Organizations.Managers;
using Marketplace.Services.Organizations.Models;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.Services.Organizations.Filters.OrganizationOwnerFilterAttribute;

namespace Marketplace.Services.Organizations.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    /*
		POST   api/organizations
		GET    api/organizations
		GET    api/organizations/{id}
		UPDATE api/organizations/{id}
		POST   api/organizations/{id}/users
		GET    api/organizations/{id}/users
	 */

    /*
	[HttpPut]
	[OrganizationOwner]
	public async Task<OrganizationModel> Update(
		Guid organizationId,
		CreateOrganizationModel organizationModel)
	{
	}*/

    private readonly OrganizationManager _organizationManager;

    public OrganizationsController(OrganizationManager organizationManager)
    {
        _organizationManager = organizationManager;
    }


    [HttpGet("getList")]
    public async Task<IActionResult> GetOrganizations()
    {

        return Ok(await _organizationManager.GetOrganizations());
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrganization(CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Create(organizationModel));
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(Guid organizationId)
    {
        return Ok(await _organizationManager.GetById(organizationId));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId, CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Update(organizationId, organizationModel));
    }

}