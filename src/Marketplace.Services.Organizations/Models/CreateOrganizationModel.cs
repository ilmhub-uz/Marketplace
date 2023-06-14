namespace Marketplace.Services.Organizations.Models;

public class CreateOrganizationModel
{
	public required string Name { get; set; }
	public string? Description { get; set; }

	public IFormFile? Logo { get; set; }
	public string? Contact { get; set; }

	public List<CreateAddressModel>? Addresses { get; set; }
}

public class CreateAddressModel
{
	public required string Address { get; set; }
}