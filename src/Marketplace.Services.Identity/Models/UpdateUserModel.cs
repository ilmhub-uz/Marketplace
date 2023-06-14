using System.ComponentModel.DataAnnotations;

namespace Marketplace.Services.Identity.Models;

public class UpdateUserModel
{
    public  string? Name { get; set; }
    public  string? UserName { get; set; }
    public  string? Password { get; set; }
}