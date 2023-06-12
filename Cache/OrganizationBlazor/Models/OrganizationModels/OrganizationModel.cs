namespace OrganizationBlazor.Models.OrganizationModels;

public class OrganizationModel
{
    public Guid Id { get; set; }
    public  string Name { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public string? Contact { get; set; }
    public List<OrganizationAddressModel>? Addresses { get; set; }
}