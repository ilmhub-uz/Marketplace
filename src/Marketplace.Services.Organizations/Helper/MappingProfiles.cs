using AutoMapper;
using Marketplace.Services.Organizations.Entities;
using Marketplace.Services.Organizations.Models;

namespace Marketplace.Services.Organizations.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Organization, OrganizationModel>();
        CreateMap<OrganizationModel, Organization>();
    }
}
