using Marketplace.Blazor.Entities;

namespace Marketplace.Services.Identity.Models;

public class UserModel
{
    public UserModel()
    {
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string UserName { get; set; }
}