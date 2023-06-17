using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Entities;
using Marketplace.Services.Organizations.FileServices;
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
		var organizations = await _organizationsDbContext.Organizations
			.Include(o => o.Addresses)
			.Include(o => o.Users)
			.ToListAsync();

		var organizationModels = new List<OrganizationModel>();
		foreach (var organization in organizations)
		{
			organizationModels.Add(ParseToOrganizationModel(organization));
		}

		return organizationModels;
	}

	public async Task<OrganizationModel> Create(CreateOrganizationModel organizationModel)
	{
		var organization = new Organization()
		{
			Id = Guid.NewGuid(),
			Name = organizationModel.Name,
			Description = organizationModel.Description,
			Logo = await FileService.SaveOrganizationLogo(organizationModel.Logo!),
			Contact = organizationModel.Contact,
		};

		if (organizationModel.Addresses != null)
		{
			organization.Addresses = organizationModel.Addresses!.Select(model => new OrganizationAddress()
			{
				Id = Guid.NewGuid(),
				OrganizationId = organization.Id,
				Organization = organization,
				Address = model.Address
			}).ToList();
		}

		var organizationUser = new OrganizationUser()
		{
			OrganizationId = organization.Id,
			UserId = _userProvider.UserId,
			UserRole = OrganizationUserRole.Owner
		};
		organization.Users = new List<OrganizationUser> { organizationUser };
		_organizationsDbContext.Organizations.Add(organization);

		await _organizationsDbContext.SaveChangesAsync();
		return ParseToOrganizationModel(organization);
	}

	public async Task<OrganizationModel> GetById(Guid organizationId)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();
		return ParseToOrganizationModel(organization!);

	}

	public async Task<OrganizationModel> Update(
		Guid organizationId,
		CreateOrganizationModel organizationModel)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();
		organization!.Name = organizationModel.Name;
		organization.Description = organizationModel.Description;
		organization.Logo = await FileService.SaveOrganizationLogo(organizationModel.Logo!);
		organization.Contact = organizationModel.Contact;
		if (organizationModel.Addresses != null)
		{
			for (int i = 0; i < organizationModel.Addresses!.Count; i++)
			{
				organization.Addresses![i].Address = organizationModel.Addresses[i].Address;
			}

		}
		await _organizationsDbContext.SaveChangesAsync();
		return ParseToOrganizationModel(organization);

	}

	private OrganizationModel ParseToOrganizationModel(Organization model)
	{
		var organizationModel = new OrganizationModel()
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			Logo = model.Logo,
			Contact = model.Contact,
			Users = model.Users,
		};

		if (model.Addresses != null)
		{

			organizationModel.Addresses = model.Addresses!
				.Select(address => new OrganizationAddressModel()
				{ Id = address.Id, Address = address.Address })
				.ToList();
		}
		return organizationModel;
	}

}