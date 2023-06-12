namespace OrganizationBlazor.Models.OrganizationModels;

public class OrganizationUser
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public OrganizationUserRole UserRole { get; set; }
}