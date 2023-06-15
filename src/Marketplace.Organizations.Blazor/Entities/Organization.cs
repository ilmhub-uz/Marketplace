namespace Marketplace.Organizations.Blazor.Entities;

public enum OrganizationUserRole
{
	Owner,
	Manager
}

public class OrganizationUser
{
	public Guid OrganizationId { get; set; }
	public Guid UserId { get; set; }
	public OrganizationUserRole UserRole { get; set; }
}

public class Organization
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }

	public string? Logo { get; set; }
	public string? Contact { get; set; }

	public List<OrganizationUser>? Users { get; set; }
	public List<OrganizationAddress>? Addresses { get; set; }
}

public class OrganizationAddress
{
	public Guid Id { get; set; }

	public Guid OrganizationId { get; set; }
	public Organization? Organization { get; set; }

	public string Address { get; set; }
}