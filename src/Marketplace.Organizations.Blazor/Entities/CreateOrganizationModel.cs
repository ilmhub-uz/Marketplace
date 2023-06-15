using Microsoft.AspNetCore.Components.Forms;

namespace Marketplace.Organizations.Blazor.Entities;

public class CreateOrganizationModel
{
	public string Name { get; set; }
	public string? Description { get; set; }

	public IBrowserFile? Logo { get; set; }
	public string? Contact { get; set; }

	public List<CreateAddressModel>? Addresses { get; set; }
}

public class CreateAddressModel
{
	public string Address { get; set; }
}