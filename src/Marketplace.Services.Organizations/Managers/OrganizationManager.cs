using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Entities;
using Marketplace.Services.Organizations.Models;
using Marketplace.Services.Organizations.Providers;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organizations.Managers;

public class OrganizationManager
{
	private readonly OrganizationsDbContext _organizationsDbContext;
	private readonly UserProvider _userProvider;

	public OrganizationManager(
		OrganizationsDbContext organizationsDbContext, 
		UserProvider userProvider)
	{
		_organizationsDbContext = organizationsDbContext;
		_userProvider = userProvider;
	}

	public async Task<List<OrganizationModel>> GetOrganizations()
	{

	}

	public async Task<OrganizationModel> Create(CreateOrganizationModel organizationModel)
	{

	}

	public async Task<OrganizationModel> GetById(Guid organizationId)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();

	}

	public async Task<OrganizationModel> Update(
		Guid organizationId, 
		CreateOrganizationModel organizationModel)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();

	}
}