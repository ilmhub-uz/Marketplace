using System.ComponentModel.DataAnnotations;

namespace Marketplace.Services.Identity.Models;

public class CreateUserModel
{
	public required string Name { get; set; }
	public required string Password { get; set; }
	[Compare(nameof(Password))]
	public required string ConfirmPassword { get; set; }
	public required string UserName { get; set; }
}