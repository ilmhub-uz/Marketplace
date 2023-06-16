using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Organizations.Blazor.Models;

public class CreateOrganizationModel
{
	public  string Name { get; set; }
	public string? Description { get; set; }

	public IBrowserFile? Logo { get; set; }
	public string? Contact { get; set; }
	public List<CreateAddressModel>? Addresses { get; set; }
}

public class CreateAddressModel
{
	public  string? Address { get; set; }
}