using System.ComponentModel.DataAnnotations;

namespace Marketplace.Blazor.AccountModels;

public class CreateUserModel
{
    public string Name { get; set; } = null!;
	public  string UserName { get; set; } = null!;
    public  string Password { get; set; } = null!;
    [Compare(nameof(Password))]
	public  string ConfirmPassword { get; set; } = null!;
}