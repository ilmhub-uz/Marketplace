namespace Marketplace.Blazor.OrganizationModels;


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



