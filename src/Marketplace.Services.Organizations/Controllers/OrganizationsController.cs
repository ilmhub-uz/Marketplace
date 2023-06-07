using Microsoft.AspNetCore.Mvc;

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
}