using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organizations.Managers;

public class OrganizationUserManager
{
	private readonly OrganizationsDbContext _context;

	public OrganizationUserManager(OrganizationsDbContext context)
	{
		_context = context;
	}

	public async Task<OrganizationUser> AddUser(Guid userId, Guid organizationId)
	{
		var organization = await _context.Organizations
			.Include(o => o.Users)
			.FirstOrDefaultAsync(o => o.Id == organizationId);
		if (organization == null)
			throw new Exception("Not found");

		var organizationUser = new OrganizationUser()
		{
			OrganizationId = organizationId,
			UserId = userId,
			UserRole = OrganizationUserRole.Manager
		};

		organization.Users!.Add(organizationUser);
		await _context.SaveChangesAsync();

		return organizationUser;
	}

	public async Task<List<OrganizationUser>> GetOrganizationUsers(Guid organizationId)
	{
		var organization = await _context.Organizations
			.Include(o => o.Users)
			.FirstOrDefaultAsync(o => o.Id == organizationId);

		if (organization == null)
			throw new Exception("Not found");

		var organizationUsers = organization.Users!.ToList();
		return organizationUsers;
	}

	public async Task<OrganizationUser>? GetOrganizationUser(Guid organizationId, Guid userId)
	{
		var organization = await _context.Organizations
			.Include(o => o.Users)
			.FirstOrDefaultAsync(o => o.Id == organizationId);

		if (organization == null)
			throw new Exception("Not found");

		var organizationUser = organization.Users!.FirstOrDefault(u => u.UserId == userId);

		return organizationUser!;
	}
}