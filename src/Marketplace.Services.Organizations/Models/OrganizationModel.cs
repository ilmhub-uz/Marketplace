using Marketplace.Services.Organizations.Entities;

namespace Marketplace.Services.Organizations.Models;

public class OrganizationUserModel
{
	public Guid UserId { get; set; }
	public OrganizationUserRole UserRole { get; set; }
}

public class OrganizationModel
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }

	public string? Logo { get; set; }
	public string? Contact { get; set; }

	public List<OrganizationUser>? Users { get; set; }
	public List<OrganizationAddressModel>? Addresses { get; set; }
}

public class OrganizationAddressModel
{
	public Guid Id { get; set; }

	public string Address { get; set; }
}