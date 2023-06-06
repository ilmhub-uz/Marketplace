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
        return organizations
            .Select(organization => new OrganizationModel()
                { Id = organization.Id,
                    Name = organization.Name, 
                    Description = organization.Description,
                    Logo = organization.Logo,
                    Contact = organization.Contact, 
                    Users = organization.Users,
                    Addresses = organization.Addresses!
                        .Select(address => new OrganizationAddressModel()
                            { Id = address.Id, Address = address.Address })
                        .ToList() })
            .ToList();
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

        organization.Addresses = organizationModel.Addresses!.Select(model => new OrganizationAddress()
        {
            Id = Guid.NewGuid(),
            OrganizationId = organization.Id,
            Organization = organization,
            Address = model.Address
        }).ToList();
        _organizationsDbContext.Organizations.Add(organization);

        await _organizationsDbContext.SaveChangesAsync();
        return new OrganizationModel()
        {
            Id = organization.Id,
            Name = organization.Name,
            Description = organization.Description,
            Logo = organization.Logo,
            Contact = organization.Contact,
            Users = organization.Users,
            Addresses = organization.Addresses!
                .Select(address => new OrganizationAddressModel()
                    { Id = address.Id, Address = address.Address })
                .ToList()
        };
    }

	public async Task<OrganizationModel> GetById(Guid organizationId)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();
        return new OrganizationModel()
        {
            Id = organization.Id,
            Name = organization.Name,
            Description = organization.Description,
            Logo = organization.Logo,
            Contact = organization.Contact,
            Users = organization.Users,
            Addresses = organization.Addresses!
                .Select(address => new OrganizationAddressModel()
                    { Id = address.Id, Address = address.Address })
                .ToList()
        };

    }

	public async Task<OrganizationModel> Update(
		Guid organizationId, 
		CreateOrganizationModel organizationModel)
	{
		var organization = await _organizationsDbContext.Organizations
			.Where(o => o.Id == organizationId)
			.FirstOrDefaultAsync();
        organization.Name = organizationModel.Name;
        organization.Description = organizationModel.Description;
        organization.Logo = await FileService.SaveOrganizationLogo(organizationModel.Logo!);
        organization.Contact = organizationModel.Contact;
        for (int i = 0; i < organizationModel.Addresses!.Count; i++)
        {
            organization.Addresses![i].Address = organizationModel.Addresses[i].Address;
        }

        await _organizationsDbContext.SaveChangesAsync();
        return new OrganizationModel()
        {
            Id = organization.Id,
            Name = organization.Name,
            Description = organization.Description,
            Logo = organization.Logo,
            Contact = organization.Contact,
            Users = organization.Users,
            Addresses = organization.Addresses!
                .Select(address => new OrganizationAddressModel()
                    { Id = address.Id, Address = address.Address })
                .ToList()
        };

    }
}